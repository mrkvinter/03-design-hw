using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ninject;
using WordsCloud.Algorithm;
using WordsCloud.Client;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud
{
    public class Program
    {
        private readonly Options options;
        private readonly IClient[] clients;
        private readonly Func<string, string, List<Word>> parser;

        public Program(Options options, params IClient[] clients)
        {
            this.options = options;
            this.clients = clients;
            if (options.FileName == null)
                Console.WriteLine("Не было введено имя файла для работы программы.");
            if (options.FileNameSaveImage == null)
                Console.WriteLine("Не было введено имя файла для сохранения итогового изображения.");
            if (options.FileNameDull == null)
                Console.WriteLine("Список скучных не бы получен.");
        }

        private static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<Options>().ToSelf().WithConstructorArgument(args);
            
            var wordsContainer =
                ToWordsContainer.FromText(File.OpenText(kernel.Get<Options>().FileName).ReadToEnd(),
                    File.OpenText(kernel.Get<Options>().FileNameDull).ReadToEnd());
            wordsContainer = SampleAlgorithm.ApplyAlgorithm(wordsContainer, 1280);

            kernel.Bind<IView>().To<ViewPngImage>();
            kernel.Bind<IClient>().To<ConsoleClient>();
            kernel.Bind<IClient>().To<GuiClient>();

            kernel.Get<Program>()
                .GetClient()
                .Run(wordsContainer);
        }

        public IClient GetClient()
        {
            return clients.FirstOrDefault(c => c.Name.Equals(options.Client, StringComparison.InvariantCultureIgnoreCase)) ?? clients.First();
        }
    }
}
