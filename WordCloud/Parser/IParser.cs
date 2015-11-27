using System.Collections.Generic;

namespace WordsCloud.Parser
{
    public interface IParser
    {
        Dictionary<string, int> Parse();
    }
}
