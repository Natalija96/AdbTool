using Commons.Constants;
using Commons.Contracts;

namespace FileSystem.CommandGenerators
{
    public class GetFilesCommandGenerator : ICommandGenerator
    {
        public string Path { get; private set; }

        public GetFilesCommandGenerator(string path)
        {
            Path = path;
        }

        public string Generate()
        {
            return string.Format(CommandPatterns.LsPattern, Path);
        }
    }
}
