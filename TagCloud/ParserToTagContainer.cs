using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public class ParserToTagContainer
    {
        private List<string> words;
        private readonly string text;
         
        public ParserToTagContainer(string text)
        {

            words = new List<string>();
            this.text = text;
        }

        public TagContainer Parse()
        {
            var words = new Dictionary<string, int>();
            foreach (var e in text.Split())
            {
                if (!words.ContainsKey(e))
                    words.Add(e, 1);
                else
                    words[e]++;
            }
            return new TagContainer(words);
        }
    }
}
