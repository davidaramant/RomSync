using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomSync.Model.Filtering;

namespace RomSync.Model
{
    public static class Filter
    {
        public static IFilter Parse(string input)
        {
            var cleanedInput = input.Trim().ToLowerInvariant();

            if( cleanedInput == String.Empty)
                return Empty;

            var allTerms = new List<IFilter>();
            var quoted = false;
            var quoteTerms = new List<string>();

            foreach (var term in cleanedInput.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (term.StartsWith("\""))
                {
                    quoted = true;
                    quoteTerms.Add(term.Remove(0,1));
                }
                else if (term.EndsWith("\""))
                {
                    quoteTerms.Add(term.Remove(term.Length-1,1));
                    quoted = false;

                    allTerms.Add(new Contains(string.Join(" ", quoteTerms)));
                    quoteTerms.Clear();
                }
                else if (quoted)
                {
                    quoteTerms.Add(term);
                }
                else
                {
                    allTerms.Add(new Contains(term));
                }
            }

            return new And(allTerms);
        }

        public static readonly IFilter Empty = new EmptyFilter();

        sealed class EmptyFilter : IFilter
        {
            public bool Matches(string gameMetadata)
            {
                return true;
            }
        }
    }
}
