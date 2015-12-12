using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordsCloud
{
    static class Client
    {
        public static void Console(List<Word> words, Func<List<Word>, int, List<Word>> algo, Func<List<Word>, Image> viewer, int width)
        {
            System.Console.WriteLine("Word container: " + (words != null ? "there's." : "none."));
            viewer(algo(words, width));
        }
    }
}
