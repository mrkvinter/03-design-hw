namespace WordsCloud.Client
{
    public interface IClient
    {
        string Name { get; }
        void Run();
    }
}
