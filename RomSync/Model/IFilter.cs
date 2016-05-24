namespace RomSync.Model
{
    public interface IFilter
    {
        bool Matches(string gameSearchString);
    }
}
