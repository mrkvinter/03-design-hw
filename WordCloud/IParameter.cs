using System.Drawing;
using System.Windows.Forms;

namespace WordsCloud
{
    public interface IParameter { }

    public class ColorWord : IParameter
    {
        public readonly Color Color;

        public ColorWord(Color color)
        {
            Color = color;
        }
    }

    public class FontWord : IParameter
    {
        public Font Font;
        public FontWord(string fontName, int size = 8)
        {
            Font = new Font(fontName, size);
        }
    }

    public class Position : IParameter
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class RectangleWord : IParameter
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

    public class Rotate : IParameter
    {
        public double Value;

        public Rotate(double rotate)
        {
            Value = rotate;
        }
    }

    public class SizeWord : IParameter
    {
        public int Value { get; private set; }

        public SizeWord(int size)
        {
            Value = size;
        }
    }
}
