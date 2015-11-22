using System.Drawing;

namespace TagCloud.Parameter
{
    public class ColorTag : IParameter
    {
        public readonly Color Color;

        public ColorTag(Color color)
        {
            Color = color;
        }
    }
}
