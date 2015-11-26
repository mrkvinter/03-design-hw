using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using TagCloud;
using TagCloud.Reader;

namespace TagCloudTest
{
    [TestFixture]
    public class TagContainerTest
    {
        [Test]
        public void MakeTagContainer_Text_TagContainer()
        {
            var data = "word1\nword1\nword2\n";

            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToTagContainer(reader);

            var expected = new TagContainer{ { "word1", 2}, { "word2", 1}  };
            var actual = new TagContainer(parser.Parse());

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void MakeTagContainer_TextWithDifferentFormWord_TagContainer()
        {
            var data = "Рыба рыбу несла";

            var reader = A.Fake<IReader>();
            A.CallTo(() => reader.ReadAll()).Returns(data);
            var parser = new ParserTextToTagContainer(reader);

            var expected = new TagContainer { { "рыба", 2 }, { "нести", 1 } };
            var actual = new TagContainer(parser.Parse());

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
