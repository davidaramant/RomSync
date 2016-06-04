using System;
using System.IO;
using RomSync.Properties;

namespace RomSync.Model
{
    static class SettingsExtensions
    {
        public static string NeoGeoPath(this Settings settings)
        {
            return Path.Combine(settings.OutputPath, "neogeo");
        }

        public static string ArcadePath(this Settings settings)
        {
            return Path.Combine(settings.OutputPath, "arcade");
        }

        public static string GetPath(this Settings settings, SyncState state)
        {
            switch (state)
            {
                case SyncState.Unsynced:
                    return settings.InputPath;
                case SyncState.NeoGeo:
                    return settings.NeoGeoPath();
                case SyncState.Arcade:
                    return settings.ArcadePath();
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
