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

            return new Contains(cleanedInput);
        }

        public static readonly IFilter Empty = new EmptyFilter();

        sealed class EmptyFilter : IFilter
        {
            public bool Matches(string gameSearchString)
            {
                return true;
            }
        }
    }
}
