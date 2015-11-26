using System;
using System.Linq;
using TagCloud.Client;
using Ninject;
using TagCloud.Algorithm;
using TagCloud;
using System.Drawing;
using Ninject.Planning.Bindings.Resolvers;
using TagCloud.Parser;
using TagCloud.Reader;

namespace TagCloud
{
    public class Program
    {
        private readonly Options options;
        private IClient[] clients;

        public Program(Options options,  params IClient[] clients)
        {
            this.options = options;
            this.clients = clients;
        }

        private static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Bind<Options>().ToSelf().WithConstructorArgument(args);
            
            kernel.Bind<IAlgorithm>().To<SampleAlgorithm>();
            kernel.Bind<TagContainer>().ToSelf();
            kernel.Bind<IParser>().To<ParserTextToTagContainer>();
            kernel.Bind<IReader>().To<FileReader>();
            kernel.Bind<IClient>().To<ConsoleClient>();
            kernel.Bind<IClient>().To<GuiClient>();

            kernel.Get<Program>()
                .GetClient()
                .Run();
        }

        public IClient GetClient()
        {
            return clients.FirstOrDefault(c => c.Name.Equals(options.Client, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
