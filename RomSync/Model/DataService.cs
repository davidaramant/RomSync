﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RomSync.Extensions;
using RomSync.Properties;

namespace RomSync.Model
{
    public class DataService : IDataService
    {
        public async Task<IEnumerable<GameState>> GetGameListAsync()
        {
            var settings = Settings.Default;

            var gameListTask = Task.Run(() =>
                DatabaseParser.ParseFile(settings.DatabaseFilePath));

            var inputRomsTask = GetFilesAsync(settings.InputPath);
            var syncedArcadeGamesTask = GetFilesAsync(settings.ArcadePath());
            var syncedNeoGeoGamesTask = GetFilesAsync(settings.NeoGeoPath());

            await gameListTask;
            await inputRomsTask;

            var availableGames = gameListTask.Result.Join(
                inputRomsTask.Result,
                info => info.ShortName,
                romFile => romFile,
                (info, romFile) => info);

            await syncedArcadeGamesTask;
            await syncedNeoGeoGamesTask;

            return availableGames.Select(
                info => new GameState(info,
                    DetermineSyncState(info,
                        arcadeGames: syncedArcadeGamesTask.Result,
                        neoGeoGames: syncedNeoGeoGamesTask.Result)));
        }

        public Task UpdateGameList(IEnumerable<Tuple<GameState, SyncState>> requestedChanges)
        {
            return Task.WhenAll(requestedChanges.Select(rc => UpdateGame(rc.Item1, rc.Item2)));
        }

        private Task UpdateGame(GameState current, SyncState newState)
        {
            var settings = Settings.Default;

            switch (newState)
            {
                case SyncState.Unsynced:
                    Debug.Assert(current.CurrentPath != current.Info.FilePath, "Attempting to delete source file");
                    return Task.Run(() => File.Delete(current.CurrentPath));

                case SyncState.NeoGeo:
                case SyncState.Arcade:
                    var copyTask = Task.Run(() =>
                                File.Copy(
                                    current.Info.FilePath,
                                    Path.Combine(settings.GetPath(newState), current.Info.FileName)));

                    if (current.CurrentState != SyncState.Unsynced)
                    {
                        return Task.WhenAll(copyTask, Task.Run(() => File.Delete(current.CurrentPath)));
                    }
                    else
                    {
                        return copyTask;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private static SyncState DetermineSyncState(GameInfo info, HashSet<string> arcadeGames, HashSet<string> neoGeoGames)
        {
            if (arcadeGames.Contains(info.ShortName))
                return SyncState.Arcade;
            if (neoGeoGames.Contains(info.ShortName))
                return SyncState.NeoGeo;
            return SyncState.Unsynced;
        }

        private static Task<HashSet<string>> GetFilesAsync(string path)
        {
            return Task.Run(() => Directory.EnumerateFiles(path).Select(Path.GetFileNameWithoutExtension).ToHashSet());
        }
    }
}