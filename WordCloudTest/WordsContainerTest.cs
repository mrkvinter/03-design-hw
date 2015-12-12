using System.Collections.Generic;
using NUnit.Framework;
using WordsCloud;

namespace TagCloudTest
{
    [TestFixture]
    public class WordsContainerTest
    {
        [Test]
        public void MakeTagContainer_Text_TagContainer()
        {
            var data = "word1\nword1\nword2\n";

            var expected = new List<Word>{ new Word("word1", 2), new Word("word2", 1) };
            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void MakeTagContainer_TextWithDifferentFormWord_TagContainer()
        {
            var data = "Рыба рыбу несла";

            var expected = new List<Word> { new Word("рыба", 2), new Word("нести", 1) };
            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
