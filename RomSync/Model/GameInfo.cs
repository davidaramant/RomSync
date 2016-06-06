using System;
using System.IO;
using RomSync.Properties;

namespace RomSync.Model
{
    public sealed class GameInfo
    {
        private readonly Lazy<long> _fileSize;

        public string ShortName { get; }
        public string LongName { get; }
        public string Manufacturer { get; }
        public string Year { get; }

        public string FileName => ShortName + ".zip";
        public string FilePath => Path.Combine(Settings.Default.InputPath, FileName);
        public long SizeInBytes => _fileSize.Value;

        public GameInfo(string shortName, string longName, string manufacturer, string year)
        {
            ShortName = shortName;
            LongName = longName;
            Manufacturer = manufacturer;
            Year = year;

            _fileSize = new Lazy<long>( ()=> 
            {
                var info = new FileInfo(FilePath);
                return info.Length;
            } );
        }
    }
}
