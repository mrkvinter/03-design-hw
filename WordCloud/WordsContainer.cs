using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace WordsCloud
{
    public class WordsContainer : IEnumerable<Word>
    {
        private readonly List<Word> words;

        public WordsContainer()
        {
            words = new List<Word>();
        }

        public WordsContainer(Dictionary<string, int> container)
        {
            words = new List<Word>();
            foreach (var word in container.Select(e => new Word(e.Key, e.Value)))
                words.Add(word);
        }

        public Word this[string name]
        {
            get
            {
                foreach (var word in words)
                    if (word.Name == name)
                        return word;
                throw new KeyNotFoundException();
            }
        }

        public void Add(string key, int value)
        {
            words.Add(new Word(key, value));
        }

        public WordsContainer Remove(Word word)
        {
            words.Remove(word);
            return this;
        }

        public IEnumerator<Word> GetEnumerator()
        {
            return words.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return words.GetEnumerator();
        }
    }
}
