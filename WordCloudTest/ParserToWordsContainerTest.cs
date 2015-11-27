using System.Collections.Generic;
using NUnit.Framework;
using WordsCloud;
using FakeItEasy;
using WordsCloud.Reader;

namespace TagCloudTest
{
    [TestFixture]
    public class ParserToWordsContainerTest
    {
        [Test]
        public void OneWordOnLine_TagContainerCountLine()
        {
            
            var data = "word1\nword2\nword3\n";
            
            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader);
             
            var expected = new Dictionary<string, int> { {"word1", 1}, {"word2", 1}, {"word3", 1} };
            var actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void OtherRegister_OneElementKey()
        {
            var data = "Hello HELLO hello hElLo";
            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader);

            var expected = new Dictionary<string, int> { { "hello", 4 }};
            var actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void EmptyText_EmptyDictionary()
        {
            var data = "";
            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader);

            var actual = parser.Parse();

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void TextWithDifferentFormWord_DictionaryOneElement()
        {
            var data = "Рыба рыбу рыбы рыбой";
            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToWordsContainer(reader);

            var expected = new Dictionary<string, int> { { "рыба", 4 } };
            var actual = parser.Parse();

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}