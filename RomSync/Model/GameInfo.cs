namespace RomSync.Model
{
    public sealed class GameInfo
    {
        public string ShortName { get; }
        public string LongName { get; }
        public string Manufacturer { get; }
        public int Year { get; }

        public GameInfo(string shortName, string longName, string manufacturer, int year)
        {
            ShortName = shortName;
            LongName = longName;
            Manufacturer = manufacturer;
            Year = year;
        }
    }
}
