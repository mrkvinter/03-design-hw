using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WordsCloud.Parser;

namespace WordsCloud.Algorithm
{
    public class SampleAlgorithm
        : IAlgorithm
    {
        public WordsContainer Container { get; }
        public int Width { get; private set; } = 1280;
        public int Height { get; private set; } = 720;


        public SampleAlgorithm(IParser parser)
        {
            Container = new WordsContainer(parser.Parse());
        }

        public WordsContainer ApplyAlgorithm()
        {
            var newAlgo = 
                     SetSizeWord()
                    .SetFontWord()
                    .SetColorWord()
                    .SetRectangleAreaWord()
                    .SetPosition();
            return newAlgo.Container;

        }

        public SampleAlgorithm SetSizeImage(int w, int h)
        {
            Width = w;
            Height = h;
            return this;
        }

        private SampleAlgorithm SetPosition()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var orderContainer = Container.OrderByDescending(e => rnd.NextDouble()).ToList();

            var shiftY = 0;
            var maxHeightWord = 0;
            var widthWordes = 0;
            var wordesOnLine = new List<Word>();
            foreach (var word in orderContainer)
            {
                var size = word.GetParameter<RectangleWord>();
                if (widthWordes + size.Width > Width)
                {
                    var maxHeightOnLine = wordesOnLine.Max(e => e.GetParameter<RectangleWord>().Height) / 2.0;
                    foreach (var e in wordesOnLine)
                    {
                        var oldX = e.GetParameter<Position>().X;
                        e.SetParameter(new Position(oldX,
                            (int)(shiftY + maxHeightOnLine - e.GetParameter<RectangleWord>().Height / 2.0)));
                    }
                    widthWordes = 0;
                    shiftY += maxHeightWord;
                    maxHeightWord = 0;
                    wordesOnLine.Clear();
                }
                if (size.Height > maxHeightWord) maxHeightWord = size.Height;
                word.SetParameter(new Position(widthWordes, shiftY));
                widthWordes += size.Width;
                wordesOnLine.Add(word);
            }
            Height = shiftY;
            return this;
        }

        private SampleAlgorithm SetSizeWord()
        {
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var count = orderContainer.Count;
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var factor = index++/ count;
                word.SetParameter(new SizeWord(10 + (int) (18*factor)));
            }

            return this;
        }

        private SampleAlgorithm SetFontWord()
        {
            foreach (var word in Container)
                word.SetParameter(new FontWord("Monaco", word.GetParameter<SizeWord>().Value));
            return this;
        }

        private SampleAlgorithm SetColorWord()
        {
            var countWord = Container.Count();
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var alpha = (int)Math.Max(255*(index++ / countWord), 40.0);
                word.SetParameter(new ColorWord(Color.FromArgb(alpha, 0, 0, 0)));
            }
            return this;
        }

        private SampleAlgorithm SetRectangleAreaWord()
        {
            foreach (var word in Container)
                word.SetParameter(new RectangleWord(word));
            return this;
        }
    }
}
