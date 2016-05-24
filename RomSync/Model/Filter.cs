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
            return new Contains(input.Trim().ToLowerInvariant());
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
