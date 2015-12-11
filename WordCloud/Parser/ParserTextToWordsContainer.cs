using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NHunspell;
using WordsCloud.Parser;

namespace WordsCloud
{
    public class ParserTextToWordsContainer
        : IParser
    {
        private readonly string text;
        private readonly List<string> dullWords;

        public ParserTextToWordsContainer(string text, string dullWords = "")
        {
            this.text = text;
            this.dullWords = dullWords.Split().ToList();
        }


        private bool isDullWord(string word)
        {
            return (word == "" ||
                   word.Length <= 2 ||
                   dullWords.Contains(word));
        }
        public List<Word> Parse()
        {
            var words = new Dictionary<string, int>();

            using (var hunspell = new Hunspell("ru_RU.aff", "ru_RU.dic"))
            {
                foreach (var e in text.ToLower().Split().Select(e => e.CleanTrim()).Where(e => !isDullWord(e)))
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
            return words.Select(e => new Word(e.Key, e.Value)).ToList();
        }
    }

    public static class StringExtension
    {
        public static string CleanTrim(this string str)
            => Regex.Replace(str, @"^\W*(\w*)\W*$", "$1");

    }
}