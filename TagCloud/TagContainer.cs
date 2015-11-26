using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace TagCloud
{
    public class TagContainer : IEnumerable<Tag>
    {
        private readonly List<Tag> tagCount;

        public TagContainer()
        {
            tagCount = new List<Tag>();
        }

        public TagContainer(Dictionary<string, int> container)
        {
            tagCount = new List<Tag>();
            foreach (var tag in container.Select(e => new Tag(e.Key, e.Value)))
                tagCount.Add(tag);
        }

        public Tag this[string name]
        {
            get
            {
                foreach (var e in tagCount)
                    if (e.Name == name)
                        return e;
                throw new KeyNotFoundException();
            }
        }

        public void Add(string key, int value)
        {
            tagCount.Add(new Tag(key, value));
        }

        public TagContainer Remove(Tag tag)
        {
            tagCount.Remove(tag);
            return this;
        }

        public IEnumerator<Tag> GetEnumerator()
        {
            return tagCount.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tagCount.GetEnumerator();
        }
    }
}
