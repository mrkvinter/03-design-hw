using System.Collections.Generic;

namespace TagCloud.Parser
{
    public interface IParser
    {
        Dictionary<string, int> Parse();
    }
}
