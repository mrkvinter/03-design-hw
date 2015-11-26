using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using TagCloud;
using TagCloud.Algorithm;
using TagCloud.Parser;

namespace TagCloudTest
{
    [TestFixture]
    public class AlgorithmTest
    {
        [Test]
        public void SetColor_Text_MaxAlphaColor()
        {
            var data = new Dictionary<string, int> { { "привет", 4 }, { "пока", 2 }, { "слово", 1 }};
            var parser = A.Fake<IParser>();        
            A.CallTo(() => parser.Parse()).Returns(data);
            var algo = new SampleAlgorithm(parser);
            var container = algo.ApplyAlgorithm();

            var expected = 255;
            var actual = (int)container["привет"].GetParameter<ColorTag>().Color.A;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SetSize_Text_Biggest()
        {
            var data = new Dictionary<string, int> { { "привет", 4 }, { "пока", 2 }, { "слово", 1 } };
            var parser = A.Fake<IParser>();
            A.CallTo(() => parser.Parse()).Returns(data);
            var algo = new SampleAlgorithm(parser);
            var container = algo.ApplyAlgorithm();

            var expected = new Tag("привет", 4);
            var actual = container.OrderBy(e => e.GetParameter<Size>().Value).Last();

            Assert.AreEqual(expected, actual);
        }
    }
}
