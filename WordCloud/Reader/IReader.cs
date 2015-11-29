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

        public FileReader(string name)
        {
            FileName = name;
        }

        public string ReadAll()
        {
            return File.Exists(FileName) ? File.OpenText(FileName).ReadToEnd() : null;
        }
    }
}
