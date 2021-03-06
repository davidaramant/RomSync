﻿namespace RomSync.Model.Filtering
{
    public sealed class Contains : IFilter
    {
        private readonly string _term;

        public Contains(string term)
        {
            _term = term;
        }

        public bool Matches(string gameMetadata)
        {
            return gameMetadata.Contains(_term);
        }
    }
}
