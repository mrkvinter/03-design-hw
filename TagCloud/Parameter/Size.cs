namespace TagCloud.Parameter
{
    public class Size : IParameter
    {
        public int Value { get; private set; }

        public Size(int size)
        {
            Value = size;
        }
    }
}
