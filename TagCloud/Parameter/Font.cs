using System.Drawing;

namespace TagCloud.Parameter
{
    public class FontTag : IParameter
    {
        public Font Font;
        public FontTag(string fontName, int size = 8)
        {
            Font = new Font(fontName, size);
        }
    }
}
