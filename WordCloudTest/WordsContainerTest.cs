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
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader, null);

            var expected = new WordsContainer{ { "word1", 2}, { "word2", 1}  };
            var actual = new WordsContainer(parser.Parse());

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void MakeTagContainer_TextWithDifferentFormWord_TagContainer()
        {
            var data = "Рыба рыбу несла";

            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader, null);

            var expected = new WordsContainer { { "рыба", 2 }, { "нести", 1 } };
            var actual = new WordsContainer(parser.Parse());

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
