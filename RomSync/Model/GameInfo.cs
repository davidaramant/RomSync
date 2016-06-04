using System.IO;
using RomSync.Properties;

namespace RomSync.Model
{
    public sealed class GameInfo
    {
        public string ShortName { get; }
        public string LongName { get; }
        public string Manufacturer { get; }
        public string Year { get; }

        public string FileName => ShortName + ".zip";
        public string FilePath => Path.Combine(Settings.Default.InputPath, FileName);

        public GameInfo(string shortName, string longName, string manufacturer, string year)
        {
            ShortName = shortName;
            LongName = longName;
            Manufacturer = manufacturer;
            Year = year;
        }
    }
}
