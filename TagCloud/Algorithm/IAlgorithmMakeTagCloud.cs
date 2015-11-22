using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Parameter;

namespace TagCloud.Algorithm
{
    public class BaseAlgorithm
    {
        private Dictionary<string, int> frequencyTags;
        public TagContainer Container { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public int Width { get; private set; } = 500;
        public int Height { get; private set; } = 500;
        private readonly List<Action> apply;
        private readonly List<Action> remove;


        public BaseAlgorithm()
        {
            apply = new List<Action>();
            remove = new List<Action>();
        }

        public BaseAlgorithm ApplyEvery<T>(Func<Tag, BaseAlgorithm, T> func) where T : IParameter
        {
            apply.Add(
                () =>
                {
                    foreach (var e in Container)
                        e.SetParameter(func(e, this));
                });
            return this;
        }
        
        public BaseAlgorithm RemoveEvery(Func<Tag, bool> func)
        {
            remove.Add(
                () =>
                {
                    foreach (var e in Container.Where(func).ToList())
                        Container.Remove(e);
                });
            return this;
        }

        public TagContainer ApplyAlgorithm(Dictionary<string, int> tags)
        {
            Container = new TagContainer();
            frequencyTags = tags;
            foreach(var e in frequencyTags)
                Container.Add(e.Key, e.Value);
            Min = tags.Min(e => e.Value);
            Max = tags.Max(e => e.Value);
            apply.ForEach(e => e());
            remove.ForEach(e => e());

            return Container;
        }

        public BaseAlgorithm SetSize(int w, int h)
        {
            Width = w;
            Height = h;
            return this;
        }      
    }
}
