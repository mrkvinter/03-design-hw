namespace TagCloud.Client
{
    public interface IClient
    {
        string Name { get; }
        void Run();
    }
}
