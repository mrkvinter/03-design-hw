using System.Collections.Generic;

namespace WordsCloud.Algorithm
{
    public interface IAlgorithm
    {
        int Height { get; }
        int Width { get; }
        List<Word> ApplyAlgorithm();
    }
}
