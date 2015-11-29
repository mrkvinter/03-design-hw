using System.Collections.Generic;

namespace WordsCloud.Parser
{
    public interface IParser
    {
        List<Word> Parse();
    }
}
