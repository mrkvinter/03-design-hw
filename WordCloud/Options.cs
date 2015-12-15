using CommandLine;
using System.Text;

namespace WordsCloud
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "Input file name for make tag cloud.")]
        public string FileName { get; set; }

        [Option('c', "client", DefaultValue = "console", HelpText = "Client for work.")]
        public string Client { get; set; }

        [Option('s', "fileSave", Required = true, HelpText = "Input file name for save iamge.")]
        public string FileNameSaveImage { get; set; }

        [Option('d', "fileDull", HelpText = "Input file name with dull words.")]
        public string FileNameDull { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new StringBuilder();
            help.Append("Keys for run: \n");
            help.Append("-f -file : Input file name for make tag cloud.\n");
            help.Append("-s -fileSave : Input file name for save iamge.\n");
            help.Append("-d -fileDull :Input file name with dull words.\n");
            return help.ToString();
        }
    }
}
