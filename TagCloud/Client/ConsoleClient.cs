using System;
using System.Collections.Generic;
using TagCloud.Algorithm;
using TagCloud.ViewTagCloud;

namespace TagCloud.Client
{
    class ConsoleClient
        : IClient
    {
        private readonly Dictionary<string, int> container;
        private readonly IAlgorithm algo;
        private readonly ViewPngImage viewer;

        public ConsoleClient(IAlgorithm algo, ViewPngImage viewer)
        {
            this.algo = algo;
            this.viewer = viewer;
        }

        public string Name => "Console";

        public void Run()
        {
            Console.WriteLine("Tag container: " + (container != null ? "there's." : "none."));
            viewer.CreateImage(algo               
                .ApplyAlgorithm());
        }
    }
}
