using System.Drawing;

namespace WordsCloud
{
    public class Word
    {
        public string Name { get; }
        public int Count { get; private set; }
        public Color Color;
        public Rectangle Rectangle;
        public int Size;
        public Font Font;


        public Word(string name, int count)
        {
            Name = name;
            Count = count;
            Color = Color.Black;
            Rectangle = Rectangle.Empty;
            Font = SystemFonts.DefaultFont;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherTag = obj as Word;
            return otherTag != null && otherTag.Name == Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
