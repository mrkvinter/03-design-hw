using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using WordsCloud;
using WordsCloud.Reader;

namespace TagCloudTest
{
    [TestFixture]
    public class WordsContainerTest
    {
        [Test]
        public void MakeTagContainer_Text_TagContainer()
        {
            var data = "word1\nword1\nword2\n";

            var reader = A.Fake<IReader>();
            var readerDull = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            A.CallTo(() => readerDull.ReadAll()).Returns(null);
            var parser = new ParserTextToWordsContainer(reader, readerDull);

            var expected = new List<Word>{ new Word("word1", 2), new Word("word2", 1) };
            var actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void MakeTagContainer_TextWithDifferentFormWord_TagContainer()
        {
            var data = "Рыба рыбу несла";

            var reader = A.Fake<IReader>();
            var readerDull = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            A.CallTo(() => readerDull.ReadAll()).Returns(null);
            var parser = new ParserTextToWordsContainer(reader, readerDull);

            var expected = new List<Word> { new Word("рыба", 2), new Word("нести", 1) };
            var actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
