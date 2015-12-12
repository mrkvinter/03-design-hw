using System.Collections.Generic;
using NUnit.Framework;
using WordsCloud;

namespace TagCloudTest
{
    [TestFixture]
    public class ParserToWordsContainerTest
    {
        [Test]
        public void OneWordOnLine_TagContainerCountLine()
        {
            
            var data = "word1\nword2\nword3\n";            
             
            var expected = new List<Word> { new Word("word1", 1), new Word("word2", 1), new Word("word3", 1) };
            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void OtherRegister_OneElementKey()
        {
            var data = "Hello HELLO hello hElLo";

            var expected = new List<Word> { new Word("hello", 4 ) };
            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void EmptyText_EmptyDictionary()
        {
            var data = "";  

            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.IsEmpty(actual);
        }

        [Test]
        public void TextWithDifferentFormWord_DictionaryOneElement()
        {
            var data = "Рыба рыбу рыбы рыбой";

            var expected = new List<Word> { new Word("рыба", 4 ) };
            var actual = ToWordsContainer.FromText(data);

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}