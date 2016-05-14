using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RomSync.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newElements)
        {
            foreach (var item in newElements)
            {
                collection.Add(item);
            }
        }
    }
}
