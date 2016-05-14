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
    }
}
