using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WordsCloud.Algorithm
{
    public static class SampleAlgorithm
    {

        public static List<Word> ApplyAlgorithm(List<Word> words, int width)
        {
            return words.SetSize()
                .SetFont()
                .SetColor()
                .SetRectangleArea()
                .SetPosition(width)
                .ToList();
        }

        private static IEnumerable<Word> SetPosition(this IEnumerable<Word> words, int widthImage )
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var shuffledWords = words.OrderByDescending(e => rnd.NextDouble());

            var shiftY = 0;
            var maxHeightWord = 0;
            var widthWords = 0;
            var wordesOnLine = new List<Word>();
            foreach (var word in shuffledWords)
            {
                var size = word.Rectangle.Size;
                if (widthWords + size.Width > widthImage)
                {
                    var maxHeightOnLine = wordesOnLine.Max(e => e.Rectangle.Height) / 2.0;
                    foreach (var wordOnLine in wordesOnLine)
                    {
                        var x = wordOnLine.Rectangle.X;
                        var y = (int) (shiftY + maxHeightOnLine - wordOnLine.Rectangle.Height/2.0);
                        wordOnLine.Rectangle.Location = new Point(x, y);
                        yield return wordOnLine;
                    }
                    widthWords = 0;
                    shiftY += maxHeightWord;
                    maxHeightWord = 0;
                    wordesOnLine.Clear();
                }
                if (size.Height > maxHeightWord) maxHeightWord = size.Height;
                word.Rectangle.Location = new Point(widthWords, shiftY);
                widthWords += size.Width;
                wordesOnLine.Add(word);
            }
            foreach (var word in wordesOnLine)
                yield return word;      
        }

        private static IEnumerable<Word> SetSize(this IEnumerable<Word> words)
        {
            var orderWords = words.OrderBy(e => e.Count).ToList();
            var count = orderWords.Count;
            var index = 1.0;
            foreach (var word in orderWords)
            {
                var factor = index++/ count;
                word.Size = 10 + (int)(18 * factor);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetFont(this IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                word.Font = new Font("Monaco", word.Size);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetColor(this IEnumerable<Word> words)
        {
            var countWord = words.Count();
            var orderWords = words.OrderBy(e => e.Count);
            var index = 1.0;
            foreach (var word in orderWords)
            {
                var alpha = (int)Math.Max(255*(index++ / countWord), 40.0);
                word.Color = Color.FromArgb(alpha, 0, 0, 0);
                yield return word;
            }
        }

        private static IEnumerable<Word> SetRectangleArea(this IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                var font = word.Font;
                var renderText = TextRenderer.MeasureText(word.Name, font);
                word.Rectangle.Size = new Size(renderText.Width, renderText.Height);
                yield return word;
            }
        }
    }
}
