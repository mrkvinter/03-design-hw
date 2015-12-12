using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordsCloud.ViewWordsCloud
{
    public static class ViewPngImage
    {
        public static Image CreateImage(List<Word> words)
        {
            Image img = new Bitmap(words.Max(e => e.Rectangle.X + e.Rectangle.Width), 
                                   words.Max(e => e.Rectangle.Y + e.Rectangle.Height));

            using (var g = Graphics.FromImage(img))
            { 
                g.Clear(Color.White);

                foreach (var word in words) 
                    g.DrawString(
                        word.Name,
                        word.Font,
                        new SolidBrush(word.Color),
                        word.Rectangle);
            }
            return img;
            //if (fileImageName != null) img.Save(fileImageName);
        }
    }
}
