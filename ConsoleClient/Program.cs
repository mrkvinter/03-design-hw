using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using CommandLine;
using WordsCloud;
using WordsCloud.Algorithm;
using WordsCloud.ViewWordsCloud;

namespace ConsoleClient
{
    class Program
    {
        public static void Main(string[] args)
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

            new ConsoleClient(options,
                        wordsContainer,
                        SampleAlgorithm.ApplyAlgorithm,
                        ViewImage.CreateImage,
                        1280)
                    .Run();
        }
    }
}
