using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomSync.Model.Filtering
{
    sealed class And : IFilter
    {
        private readonly IEnumerable<IFilter> _subFilters;

        public And(IEnumerable<IFilter> subFilters)
        {
            _subFilters = subFilters;
        }

        public bool Matches(string gameMetadata)
        {
            return _subFilters.All( f=>f.Matches(gameMetadata));
        }
    }
}
