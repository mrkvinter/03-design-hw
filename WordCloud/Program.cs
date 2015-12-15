using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using WordsCloud.Algorithm;
using WordsCloud.ViewWordsCloud;
using CommandLine;

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
                Console.WriteLine("Список скучных не был получен.");
        }

        private static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine("Не были переданы необходимые параметры.");
                options.GetUsage();
                return;
            }

            var text = File.OpenText(options.FileName).ReadToEnd();
            var dullText = File.Exists(options.FileNameDull) ? File.OpenText(options.FileNameDull).ReadToEnd() : "";
            var wordsContainer = ToWordsContainer.FromText(text, dullText);

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
