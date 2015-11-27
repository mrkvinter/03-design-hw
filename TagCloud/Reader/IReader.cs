using System.IO;

namespace WordsCloud.Reader
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
