using System.Collections.Generic;
using System.Linq;
using TagCloud;

namespace TagCloud
{
    public class Tag
    {
        public string Name { get; private set; }
        public int Count { get; private set; }
        private List<Parameters> parameters;

        public Tag(string name, int Count)
        {
            Name = name;
            this.Count = Count;
            parameters = new List<Parameters>();
        }

        public T GetParameter<T>()
        {
            foreach (var e in parameters)
                if (e is T)
                    return (T)e;
            return default(T);
        }
        public void SetParameter<T>(T parameter) where T : Parameters
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
            var otherTag = obj as Tag;
            return otherTag != null && otherTag.Name == Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
