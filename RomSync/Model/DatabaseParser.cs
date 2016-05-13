using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RomSync.Model
{
    public static class DatabaseParser
    {
        public static IEnumerable<GameInfo> ParseFile(string filePath)
        {
            using (var inputStream = File.OpenRead(filePath))
            {
                return
                    XDocument.Load(inputStream).
                        Element("datafile").
                        Elements("game").
                        Select(game => new GameInfo(
                            shortName: game.Attribute("name").Value,
                            longName: game.Element("description").Value,
                            manufacturer: game.Element("manufacturer").Value,
                            year: int.Parse(game.Element("year").Value)));
            }
        }
    }
}
