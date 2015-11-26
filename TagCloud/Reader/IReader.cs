using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Reader
{
    public interface IReader
    {
        string ReadAll();
    }

    public class FileReader
        : IReader
    {
        private string fileName { get; }

        public FileReader(Options options)
        {
            fileName = options.FileName;
        }

        public string ReadAll()
        {
            return File.OpenText(fileName).ReadToEnd();
        }
    }
}
