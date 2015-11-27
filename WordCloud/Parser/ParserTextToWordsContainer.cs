using System.Collections.Generic;
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
        private readonly List<string> dullWords;

        public ParserTextToWordsContainer(IReader readerText, IReader dullWords)
        {
            text = readerText.ReadAll();
            this.dullWords = dullWords?.ReadAll().Split().ToList() ?? new List<string>();
        }


        private bool isDullWord(string word)
        {
            return !(word == "" || 
                   word.Length <= 2 || 
                   dullWords.Contains(word));
        }
        public Dictionary<string, int> Parse()
        {
            var words = new Dictionary<string, int>();
            var removeChar = new [] {".", "(", ")", ",", ":"};
            var newText = removeChar.Aggregate(text, (current, c) => current.Replace(c, ""));
            using (var hunspell = new Hunspell("ru_RU.aff", "ru_RU.dic"))
            {
                foreach (var e in newText.ToLower().Split().Where(isDullWord))
                {
                    var beginWord = hunspell.Stem(e);
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
