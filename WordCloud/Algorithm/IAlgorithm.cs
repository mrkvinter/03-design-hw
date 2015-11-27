namespace WordsCloud.Algorithm
{
    public interface IAlgorithm
    {
        int Height { get; }
        int Width { get; }
        WordsContainer ApplyAlgorithm();
    }
}
