using System.Drawing;
using WordsCloud.Algorithm;

namespace WordsCloud.ViewWordsCloud
{
    public class ViewPngImage
        : IView
    {
        private readonly string fileImageName;

        public ViewPngImage(Options options)
        {
            fileImageName = options.FileNameSaveImage;
        }

        public void CreateImage(IAlgorithm algo)
        {
            var words = algo.ApplyAlgorithm();
            Image img = new Bitmap(algo.Width, algo.Height);
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

            if (fileImageName != null) img.Save(fileImageName);
        }
    }
}
