using System;
using System.Linq;
using Ninject;
using WordsCloud.Algorithm;
using WordsCloud.Client;
using WordsCloud.Parser;
using WordsCloud.Reader;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud
{
    public class Program
    {
        private readonly Options options;
        private readonly IClient[] clients;

        public Program(Options options,  params IClient[] clients)
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
            kernel.Bind<IAlgorithm>().To<SampleAlgorithm>();

            kernel.Bind<IParser>().To<ParserTextToWordsContainer>()
                .WithConstructorArgument("readerText",
                                         context => new FileReader(context.Kernel.Get<Options>().FileName)
                                         )
                .WithConstructorArgument("dullWords",
                                         context => new FileReader(context.Kernel.Get<Options>().FileNameDull));

            kernel.Bind<IView>().To<ViewPngImage>();
            kernel.Bind<IClient>().To<ConsoleClient>();
            kernel.Bind<IClient>().To<GuiClient>();

            kernel.Get<Program>()
                .GetClient()
                .Run();
        }

        public IClient GetClient()
        {
            return clients.FirstOrDefault(c => c.Name.Equals(options.Client, StringComparison.InvariantCultureIgnoreCase)) ?? clients.First();
        }
    }
}
