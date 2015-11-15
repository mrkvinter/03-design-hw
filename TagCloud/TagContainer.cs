using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class TagContainer
    {
        public readonly Dictionary<string, int> TagCount;

        public TagContainer()
        {
            TagCount = new Dictionary<string, int>();
        }

        public TagContainer(Dictionary<string, int> tagCount)
        {
            TagCount = tagCount;
        }
    }
}
