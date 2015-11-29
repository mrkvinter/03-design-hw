using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using WordsCloud;
using WordsCloud.Algorithm;
using WordsCloud.Parser;

namespace TagCloudTest
{
    [TestFixture]
    public class AlgorithmTest
    {
        [Test]
        public void SetColor_Text_MaxAlphaColor()
        {
            var data = new List<Word> { new Word("привет", 4), new Word("пока", 2), new Word("слово", 1 )};
            var parser = A.Fake<IParser>();        
            A.CallTo(() => parser.Parse()).Returns(data);
            var algo = new SampleAlgorithm(parser);
            var container = algo.ApplyAlgorithm();

            var expected = 255;
            var actual = (int)container.First(e => e.Name == "привет").Color.A;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SetSize_Text_Biggest()
        {
            var data = new List<Word> { new Word("привет", 4), new Word("пока", 2), new Word("слово", 1) };
            var parser = A.Fake<IParser>();
            A.CallTo(() => parser.Parse()).Returns(data);
            var algo = new SampleAlgorithm(parser);
            var container = algo.ApplyAlgorithm();

            var expected = new Word("привет", 4);
            var actual = container.OrderBy(e => e.Size).Last();

            Assert.AreEqual(expected, actual);
        }
    }
}
