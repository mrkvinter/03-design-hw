using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Parser;

namespace TagCloud.Algorithm
{
    public class SampleAlgorithm
        : IAlgorithm
    {
        public TagContainer Container { get; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public int Width { get; private set; } = 1280;
        public int Height { get; private set; } = 720;


        public SampleAlgorithm(IParser parser)
        {
            Container = new TagContainer(parser.Parse());
        }

        public TagContainer ApplyAlgorithm()
        {
            var newAlgo = this
                .remove()
                .setSizeTag()
                .setFontTag()
                .setColorTag()
                .setRectangleAreaTag()
                .setPosition();
            return newAlgo.Container;

        }
        private SampleAlgorithm remove()
        {
            foreach (var tag in Container.ToList().Where(tag => tag.Name.Length <= 2))
                Container.Remove(tag);
            return this;
        }

        public SampleAlgorithm SetSizeImage(int w, int h)
        {
            Width = w;
            Height = h;
            return this;
        }

        private SampleAlgorithm setPosition()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var orderContainer = Container.OrderByDescending(e => rnd.NextDouble()).ToList();

            var shiftY = 0;
            var maxHeightTag = 0;
            var widthTags = 0;
            var tagsOnLine = new List<Tag>();
            foreach (var tag in orderContainer)
            {
                var size = tag.GetParameter<RectangleTag>();
                if (widthTags + size.Width > Width)
                {
                    var maxHeightOnLine = tagsOnLine.Max(e => e.GetParameter<RectangleTag>().Height) / 2.0;
                    foreach (var e in tagsOnLine)
                    {
                        var oldX = e.GetParameter<Position>().X;
                        e.SetParameter(new Position(oldX,
                            (int)(shiftY + maxHeightOnLine - e.GetParameter<RectangleTag>().Height / 2.0)));
                    }
                    widthTags = 0;
                    shiftY += maxHeightTag;
                    maxHeightTag = 0;
                    tagsOnLine.Clear();
                }
                if (size.Height > maxHeightTag) maxHeightTag = size.Height;
                tag.SetParameter(new Position(widthTags, shiftY));
                widthTags += size.Width;
                tagsOnLine.Add(tag);
            }
            Height = shiftY;
            return this;
        }

        private SampleAlgorithm setSizeTag()
        {
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var count = orderContainer.Count;
            var index = 1.0;
            foreach (var tag in orderContainer)
            {
                var factor = index++/ count;
                tag.SetParameter(new Size(10 + (int) (18*factor)));
            }

            return this;
        }

        private SampleAlgorithm setFontTag()
        {
            foreach (var tag in Container)
                tag.SetParameter(new FontTag("Monaco", tag.GetParameter<Size>().Value));
            return this;
        }

        private SampleAlgorithm setColorTag()
        {
            var countTag = Container.Count();
            var orderContainer = Container.OrderBy(e => e.Count).ToList();
            var index = 1.0;
            foreach (var tag in orderContainer)
            {
                var alpha = (int)Math.Max(255*(index++ / countTag), 40.0);
                tag.SetParameter(new ColorTag(Color.FromArgb(alpha, 0, 0, 0)));
            }
            return this;
        }

        private SampleAlgorithm setRectangleAreaTag()
        {
            foreach (var tag in Container)
                tag.SetParameter(new RectangleTag(tag));
            return this;
        }
    }
}
