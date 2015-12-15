using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordsCloud
{
    static class Client
    {
        public static void Console(List<Word> words, Func<List<Word>, int, List<Word>> algo, Func<List<Word>, Image> painter, int widthImage, string imageNameFile)
        {
            System.Console.WriteLine("Word container: " + (words != null ? "there's." : "none."));
            painter(algo(words, widthImage)).Save(imageNameFile);
            System.Console.WriteLine($"Изображение сохранено в {imageNameFile}");
        }
    }
}
