using System;
using System.Linq;
using TagCloud.Client;
using Ninject;
using TagCloud.Algorithm;
using TagCloud.Parameter;
using System.Drawing;
using Ninject.Planning.Bindings.Resolvers;

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
            
            kernel.Bind<BaseAlgorithm>()
                .ToConstant(
                 new BaseAlgorithm()
                    .SetSize(1920, 1080)
                    .ApplyEvery((tag, self) => 
                        new Parameter.Size((int)(6 + 24 * ((double)(self.Container.OrderBy(e => e.Count).ToList().IndexOf(tag)) / (self.Container.Count())))))
                    .ApplyEvery((tag, self) => new FontTag("Ario", tag.GetParameter<Parameter.Size>().Value))
                    .ApplyEvery((tag, self) => 
                        new ColorTag(
                            Color.FromArgb(
                                Math.Max((int)(255 * ((double)(self.Container.OrderBy(e => e.Count).ToList().IndexOf(tag)) / (self.Container.Count()))), 50), 200, 100, 0)))
                    .ApplyEvery((tag, self) => new RectangleTag(tag))
                    .ApplyEvery((tag, self) => new Rotate(100))
                    .SetPosition()
                    .RemoveEvery(tag => tag.Name.Length <= 3));

            kernel.Bind<TagContainer>().ToSelf();
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
    public static class AlgoExtansions
    {
        public static BaseAlgorithm SetPosition(this BaseAlgorithm algo)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            Func<Tag, BaseAlgorithm, Position> apply = (tag, self) =>
            {
                var rect = tag.GetParameter<RectangleTag>();
                int x, y;
                var countRun = 0;
                do
                {
                    countRun++;
                    x = (int)((self.Width) * rnd.NextDouble());
                    y = (int)((self.Height) * rnd.NextDouble());
                    if (x + rect.Width > self.Width) x -= x + rect.Width - self.Width;
                    if (y + rect.Height > self.Height) y -= y + rect.Height - self.Height;
                } while (countRun < 100 && self.Container
                            .Where(e => e.GetParameter<Position>() != null)
                            .Select(e =>
                                new Rectangle(e.GetParameter<Position>().X,
                                              e.GetParameter<Position>().Y,
                                              e.GetParameter<RectangleTag>().Width,
                                              e.GetParameter<RectangleTag>().Height))
                            .Count(e => e.IntersectsWith(new Rectangle(x, y, rect.Width, rect.Height))) != 0);
                return new Position(x, y);
            };
            return algo.ApplyEvery(apply);
        }
    }
}
