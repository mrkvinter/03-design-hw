using System;
using System.Collections.Generic;
using WordsCloud.Algorithm;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud.Client
{
    class ConsoleClient
        : IClient
    {
        private readonly Dictionary<string, int> container;
        private readonly IView viewer;

        public ConsoleClient(IView viewer)
        {
            this.viewer = viewer;
        }

        public string Name => "Console";

        public void Run(List<Word> words )
        {
            Console.WriteLine("Word container: " + (container != null ? "there's." : "none."));
            viewer.CreateImage(words);
        }
    }
}
