using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    //Не лишний ли класс?
    public class ReaderTags
    {
        private TextReader reader;

        public ReaderTags(TextReader reader)
        {
            this.reader = reader;
        }

        public string ReadAll()
        {
            return reader.ReadToEnd();
        }
    }
}
