using Commons;
using DigitalnaForenzikaAdb.Constants;

namespace FileSystem
{
    public class PullFileCommandGenerator : ICommandGenerator
    {
        public string Path { get; private set; }
        public string SelectedPath { get; private set; }

        public PullFileCommandGenerator(string path, string selectedPath)
        {
            Path = path;
            SelectedPath = selectedPath;
        }

        public string Generate()
        {
            SelectedPath = SelectedPath.Replace("\\", "/");
            SelectedPath = $"\"{SelectedPath}\"";
            return string.Format(CommandPatterns.PullPattern, Path, SelectedPath);
        }
    }
}
