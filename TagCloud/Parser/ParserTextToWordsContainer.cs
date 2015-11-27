using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;
using WordsCloud.Parser;
using WordsCloud.Reader;

namespace WordsCloud
{
    public class ParserTextToWordsContainer
        : IParser
    {
        private readonly string text;

        public ParserTextToWordsContainer(IReader reader)
        {
            text = reader.ReadAll();
        }

        public Dictionary<string, int> Parse()
        {
            var words = new Dictionary<string, int>();
            var removeChar = new [] {".", "(", ")", ",", ":"};
            var newText = removeChar.Aggregate(text, (current, c) => current.Replace(c, ""));
            using (var hunspell = new Hunspell("ru_RU.aff", "ru_RU.dic"))
            {
                foreach (var e in newText.ToLower().Split().Where(e => e != ""))
                {
                    var beginWord = hunspell.Stem(e);
                    var a = hunspell.Analyze(e);
                    var word = e;
                    if (beginWord.Count == 1) word = beginWord[0];
                    if (!words.ContainsKey(word))
                        words.Add(word, 1);
                    else
                        words[word]++;
                }
            }
            return words;
        }
    }
}
