using System.Collections.Generic;
using System.Linq;

namespace WordsCloud
{
    public class Word
    {
        public string Name { get; }
        public int Count { get; private set; }
        private readonly List<IParameter> parameters;

        public Word(string name, int count)
        {
            Name = name;
            Count = count;
            parameters = new List<IParameter>();
        }

        public T GetParameter<T>()
        {
            foreach (var e in parameters)
                if (e is T)
                    return (T)e;
            return default(T);
        }
        public void SetParameter<T>(T parameter) where T : IParameter
        {
            foreach (var param in parameters.ToList().OfType<T>())
                parameters.Remove(param);
            parameters.Add(parameter);
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
