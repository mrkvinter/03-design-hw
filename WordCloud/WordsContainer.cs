using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace WordsCloud
{
    public class WordsContainer : IEnumerable<Word>
    {
        private readonly List<Word> tagCount;

        public WordsContainer()
        {
            tagCount = new List<Word>();
        }

        public WordsContainer(Dictionary<string, int> container)
        {
            tagCount = new List<Word>();
            foreach (var tag in container.Select(e => new Word(e.Key, e.Value)))
                tagCount.Add(tag);
        }

        public Word this[string name]
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
            tagCount.Add(new Word(key, value));
        }

        public WordsContainer Remove(Word word)
        {
            tagCount.Remove(word);
            return this;
        }

        public IEnumerator<Word> GetEnumerator()
        {
            return tagCount.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tagCount.GetEnumerator();
        }
    }
}
