using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WordsCloud.Algorithm
{
    public class SampleAlgorithm
        : IAlgorithm
    {
        public List<Word> Container { get; }
        public int Width { get; private set; } = 1280;

        public int Height
        {
            get { return 10 + Container.Max(e => e.Rectangle.Y + e.Rectangle.Height); }
        }


        public SampleAlgorithm(List<Word> container)
        {
            Container = container;
        }

        public List<Word> ApplyAlgorithm()
        {
            SetSizeWord();
            SetFontWord();
            SetColorWord();
            SetRectangleAreaWord();
            SetPosition();
            return Container;
        }

        public void SetSizeImage(int w)
        {
            Width = w;
        }

        private void SetPosition()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var orderContainer = Container.OrderByDescending(e => rnd.NextDouble()).ToList();

            var shiftY = 0;
            var maxHeightWord = 0;
            var widthWordes = 0;
            var wordesOnLine = new List<Word>();
            foreach (var word in orderContainer)
            {
                var size = word.Rectangle;
                if (widthWordes + size.Width > Width)
                {
                    var maxHeightOnLine = wordesOnLine.Max(e => e.Rectangle.Height) / 2.0;
                    foreach (var e in wordesOnLine)
                    {
                        var oldX = e.Rectangle.X;
                        e.Rectangle.Location = new Point(oldX, (int)(shiftY + maxHeightOnLine - e.Rectangle.Height / 2.0));
                    }
                    widthWordes = 0;
                    shiftY += maxHeightWord;
                    maxHeightWord = 0;
                    wordesOnLine.Clear();
                }
                if (size.Height > maxHeightWord) maxHeightWord = size.Height;
                word.Rectangle.Location = new Point(widthWordes, shiftY);
                widthWordes += size.Width;
                wordesOnLine.Add(word);
            }
        }

        private void SetSizeWord()
        {
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var count = orderContainer.Count;
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var factor = index++/ count;
                word.Size = 10 + (int)(18 * factor);
            }
        }

        private void SetFontWord()
        {
            foreach (var word in Container)
                word.Font = new Font("Monaco", word.Size);
        }

        private void SetColorWord()
        {
            var countWord = Container.Count;
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var alpha = (int)Math.Max(255*(index++ / countWord), 40.0);
                word.Color = Color.FromArgb(alpha, 0, 0, 0);
            }
        }

        private void SetRectangleAreaWord()
        {
            foreach (var word in Container)
            {
                var font = word.Font;
                var renderText = TextRenderer.MeasureText(word.Name, font);

                word.Rectangle.Size = new Size(renderText.Width, renderText.Height);
            }
        }
    }
}
