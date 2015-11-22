using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class ParserToTagContainer
    {
        private readonly string text;

        public ParserToTagContainer(Options options)
        {
            text = File.OpenText(options.FileName).ReadToEnd();
        }

        public Dictionary<string, int> Parse()
        {
            var words = new Dictionary<string, int>();

            foreach (var e in text.Split().Select(e => e.Trim('.', '(', ')', ',', ':').ToLower()).Where(e => e != ""))
            {
                if (!words.ContainsKey(e))
                    words.Add(e, 1);
                else
                    words[e]++;
            }

            return words;
        }
    }
}
