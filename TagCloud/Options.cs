﻿using CommandLine;
using System.Text;

namespace TagCloud
{
    public class Options
    {
        public Options(string[] args)
        {

            CommandLine.Parser.Default.ParseArguments(args, this);
        }

        [Option('f', "file", Required = true, HelpText = "Input file name for make tag cloud.")]
        public string FileName { get; set; }

        [Option('c', "client", DefaultValue = "console", HelpText = "Client for work.")]
        public string Client { get; set; }

        [Option('s', "fileSave", Required = true, HelpText = "Input file name for save iamge.")]
        public string FileNameSaveImage { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("TagCloud Creator.");
            usage.AppendLine("==================================");
            usage.AppendLine("Version Application: 0.01b");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }
}