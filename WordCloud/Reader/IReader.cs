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
        private string FileName { get; }

        public FileReader(Options options)
        {
            FileName = options.FileName;
        }

        public string ReadAll()
        {
            return File.OpenText(FileName).ReadToEnd();
        }
    }
}
