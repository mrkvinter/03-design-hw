using System.Drawing;
using System.Windows.Forms;

namespace TagCloud
{
    public interface Parameters { }

    public class ColorTag : Parameters
    {
        public readonly Color Color;

        public ColorTag(Color color)
        {
            Color = color;
        }
    }

    public class FontTag : Parameters
    {
        public Font Font;
        public FontTag(string fontName, int size = 8)
        {
            Font = new Font(fontName, size);
        }
    }

    public class Position : Parameters
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class RectangleTag : Parameters
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
    }

    public class Rotate : Parameters
    {
        public double Value;

        public Rotate(double rotate)
        {
            Value = rotate;
        }
    }

    public class Size : Parameters
    {
        public int Value { get; private set; }

        public Size(int size)
        {
            Value = size;
        }
    }
}
