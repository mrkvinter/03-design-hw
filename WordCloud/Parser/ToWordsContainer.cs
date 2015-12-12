using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NHunspell;

namespace WordsCloud
{
    public static class ToWordsContainer
    {
        public static List<Word> FromText(string text, string dullWords = "")
        {
            var words = new Dictionary<string, int>();
            var dulls = dullWords.Split().ToList();
            Func<string, bool> isDullWord = e => (e == "" | e.Length <= 2 || dulls.Contains(e));

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