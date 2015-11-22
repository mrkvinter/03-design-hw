using System.Collections.Generic;
using TagCloud.Parameter;

namespace TagCloud
{
    public class Tag
    {
        public string Name { get; private set; }
        public int Count { get; private set; }
        private List<IParameter> parameters;

        public Tag(string name, int Count)
        {
            Name = name;
            this.Count = Count;
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
    }
}
