using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.Parameter
{
    public class RectangleTag : IParameter
    {
        public int Width;
        public int Height;

        public RectangleTag(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public RectangleTag(Tag tag)
        {
            var font = tag.GetParameter<FontTag>()?.Font ?? SystemFonts.DefaultFont;

            var r = TextRenderer.MeasureText(tag.Name, font);
            Width = r.Width;
            Height = r.Height;
        }

        public static implicit operator System.Drawing.Rectangle(RectangleTag tag)
        {
            return new System.Drawing.Rectangle(0, 0, tag.Width, tag.Height);
        }
    }
}
