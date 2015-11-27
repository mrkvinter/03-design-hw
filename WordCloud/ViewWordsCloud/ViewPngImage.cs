using System.Drawing;
using WordsCloud.Algorithm;

namespace WordsCloud.ViewWordsCloud
{
    public class ViewPngImage
        : IView
    {
        private readonly int height;
        private readonly int width;
        private readonly string fileImageName;

        public ViewPngImage(IAlgorithm algo, Options options)
        {    
            height = algo.Height;
            width = algo.Width;
            fileImageName = options.FileNameSaveImage;
        }

        public void CreateImage(WordsContainer words)
        {
            Image img = new Bitmap(width, height);
            using (var g = Graphics.FromImage(img))
            { 
                g.Clear(Color.White);

                foreach (var word in words)
                {
                    var size = word.GetParameter<RectangleWord>() ?? new RectangleWord(100, 100);
                    var position = word.GetParameter<Position>();
                    var fontWord = word.GetParameter<FontWord>()?.Font ?? SystemFonts.DefaultFont;
                    var color = word.GetParameter<ColorWord>()?.Color ?? Color.Black;

                    g.DrawString(
                        word.Name, 
                        fontWord,
                        new SolidBrush(color), 
                        new Rectangle(position.X, position.Y, size.Width, size.Height));
                }
            }
            img.Save(fileImageName);
        }
    }
}
