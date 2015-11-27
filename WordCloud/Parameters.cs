using System.Drawing;
using System.Windows.Forms;

namespace WordsCloud
{
    public interface Parameters { }

    public class ColorWord : Parameters
    {
        public readonly Color Color;

        public ColorWord(Color color)
        {
            Color = color;
        }
    }

    public class FontWord : Parameters
    {
        public Font Font;
        public FontWord(string fontName, int size = 8)
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

    public class RectangleWord : Parameters
    {
        public int Width;
        public int Height;

        public RectangleWord(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public RectangleWord(Word word)
        {
            var font = word.GetParameter<FontWord>()?.Font ?? SystemFonts.DefaultFont;

            var r = TextRenderer.MeasureText(word.Name, font);
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

    public class SizeWord : Parameters
    {
        public int Value { get; private set; }

        public SizeWord(int size)
        {
            Value = size;
        }
    }
}
