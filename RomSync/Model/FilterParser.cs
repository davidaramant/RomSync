using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomSync.Model
{
    public static class FilterParser
    {
        public static IEnumerable<string> Parse(string input)
        {
            return input.ToLowerInvariant().Split(new [] {' '},StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
