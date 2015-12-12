using System.Collections.Generic;

namespace WordsCloud.Client
{
    public interface IClient
    {
        string Name { get; }
        void Run(List<Word> words );
    }
}
