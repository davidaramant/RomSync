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
            var buffer = new StringBuilder();

            bool insideQuotedString = false;

            foreach (var c in input.ToLowerInvariant())
            {
                switch (c)
                {
                    case '"':
                        insideQuotedString = !insideQuotedString;
                        break;
                    case ' ':
                        if (insideQuotedString)
                        {
                            buffer.Append(c);
                        }
                        else
                        {
                            if (buffer.Length > 0)
                            {
                                yield return buffer.ToString();
                                buffer.Clear();
                            }
                        }
                        break;
                    default:
                        buffer.Append(c);
                        break;
                }
            }

            if (buffer.Length > 0)
            {
                yield return buffer.ToString();
            }
        }
    }
}
