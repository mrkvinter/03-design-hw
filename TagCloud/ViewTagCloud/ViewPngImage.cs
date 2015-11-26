﻿using System.Drawing;
using System.Windows.Forms;
using TagCloud.Algorithm;
using TagCloud;

namespace TagCloud.ViewTagCloud
{
    public class ViewPngImage
        : IView
    {
        private readonly int height;
        private readonly int width;
        private string fileImageName;

        public ViewPngImage(SampleAlgorithm algo, Options options)
        {    
            height = algo.Height;
            width = algo.Width;
            fileImageName = options.FileNameSaveImage;
        }

        public void CreateImage(TagContainer tags)
        {
            Image img = new Bitmap(width, height);
            using (var g = Graphics.FromImage(img))
            { 
                g.Clear(Color.White);

                foreach (var tag in tags)
                {
                    var size = tag.GetParameter<RectangleTag>() ?? new RectangleTag(100, 100);
                    var position = tag.GetParameter<Position>();
                    var fontTag = tag.GetParameter<FontTag>()?.Font ?? SystemFonts.DefaultFont;
                    var color = tag.GetParameter<ColorTag>()?.Color ?? Color.Black;

                    var text = new Bitmap(size.Width, size.Height);
                    var graphText = Graphics.FromImage(text);
                     
                    g.DrawString(
                        tag.Name, 
                        fontTag,
                        new SolidBrush(color), 
                        new Rectangle(position.X, position.Y, size.Width, size.Height));
                }
            }
            img.Save(fileImageName);
        }
    }
}