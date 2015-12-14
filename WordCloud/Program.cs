using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using WordsCloud.Algorithm;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud
{
    public class Program
    {
        public Action Run; 

        public Program(Options options,
            List<Word> words,
            Action<List<Word>, Func<List<Word>,int, List<Word>>, Func<List<Word>, Image>, int, string> client,
            Func<List<Word>, int, List<Word>> algo,
            Func<List<Word>, Image> view,
            int width)
        {
            Run = () => client(words, algo, view, width, options.FileNameSaveImage);
            
            if (options.FileName == null)
                Console.WriteLine("Не было введено имя файла для работы программы.");
            if (options.FileNameSaveImage == null)
                Console.WriteLine("Не было введено имя файла для сохранения итогового изображения.");
            if (options.FileNameDull == null)
                Console.WriteLine("Список скучных не бы получен.");
        }

        private static void Main(string[] args)
        {
            var options = new Options(args);
            
            var wordsContainer =
                ToWordsContainer.FromText(File.OpenText(options.FileName).ReadToEnd(),
                                          File.OpenText(options.FileNameDull).ReadToEnd());

            new Program(options,
                wordsContainer,
                Client.Console,
                SampleAlgorithm.ApplyAlgorithm,
                ViewImage.CreateImage,
                1280)
                .Run();
        }
    }
}
