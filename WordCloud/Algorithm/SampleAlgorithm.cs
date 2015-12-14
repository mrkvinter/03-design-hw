using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WordsCloud.Algorithm
{
    public static class SampleAlgorithm
    {

        public static List<Word> ApplyAlgorithm(List<Word> container, int width)
        {
            return container.SetSizeWord()
                .SetFontWord()
                .SetColorWord()
                .SetRectangleAreaWord()
                .SetPosition(width)
                .ToList();
        }

        private static IEnumerable<Word> SetPosition(this IEnumerable<Word> container, int width )
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var orderContainer = container.OrderByDescending(e => rnd.NextDouble()).ToList();

            var shiftY = 0;
            var maxHeightWord = 0;
            var widthWordes = 0;
            var wordesOnLine = new List<Word>();
            foreach (var word in orderContainer)
            {
                var size = word.Rectangle.Size;
                if (widthWordes + size.Width > width)
                {
                    var maxHeightOnLine = wordesOnLine.Max(e => e.Rectangle.Height) / 2.0;
                    foreach (var wordOnLine in wordesOnLine)
                    {
                        var oldX = wordOnLine.Rectangle.X;
                        wordOnLine.Rectangle.Location = new Point(oldX, (int)(shiftY + maxHeightOnLine - wordOnLine.Rectangle.Height / 2.0));
                        yield return wordOnLine;
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
            foreach (var word in wordesOnLine)
                yield return word;      
        }

        private static IEnumerable<Word> SetSizeWord(this IEnumerable<Word> container)
        {
            var orderContainer = container.OrderBy(e => e.Count).ToList();
            var count = orderContainer.Count;
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var factor = index++/ count;
                word.Size = 10 + (int)(18 * factor);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetFontWord(this IEnumerable<Word> container)
        {
            foreach (var word in container)
            {
                word.Font = new Font("Monaco", word.Size);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetColorWord(this IEnumerable<Word> container)
        {
            var countWord = container.Count();
            var orderContainer = container.OrderBy(e => e.Count).ToList();
            var index = 1.0;
            foreach (var word in orderContainer)
            {
                var alpha = (int)Math.Max(255*(index++ / countWord), 40.0);
                word.Color = Color.FromArgb(alpha, 0, 0, 0);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetRectangleAreaWord(this IEnumerable<Word> container)
        {
            foreach (var word in container)
            {
                var font = word.Font;
                var renderText = TextRenderer.MeasureText(word.Name, font);
                word.Rectangle.Size = new Size(renderText.Width, renderText.Height);
                yield return word;
            }
        }
    }
}
