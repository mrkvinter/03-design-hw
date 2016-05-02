using System;
using System.Collections.Generic;
using System.Drawing;
using WordsCloud;

namespace ConsoleClient
{
    class ConsoleClient
    {
        public Action Run;

        public ConsoleClient(Options options,
            List<Word> words,
            Func<List<Word>, int, List<Word>> algo,
            Func<List<Word>, Image> view,
            int width)
        {
            Run = () => Console(words, algo, view, width, options.FileNameSaveImage);

            if (options.FileName == null)
                System.Console.WriteLine("Не было введено имя файла для работы программы.");
            if (options.FileNameSaveImage == null)
                System.Console.WriteLine("Не было введено имя файла для сохранения итогового изображения.");
            if (options.FileNameDull == null)
                System.Console.WriteLine("Список скучных не был получен.");
        }
        public static void Console(List<Word> words, Func<List<Word>, int, List<Word>> algo, Func<List<Word>, Image> painter, int widthImage, string imageNameFile)
        {
            System.Console.WriteLine("Word container: " + (words != null ? "there's." : "none."));
            painter(algo(words, widthImage)).Save(imageNameFile);
            System.Console.WriteLine($"Изображение сохранено в {imageNameFile}");
        }
    }
}
