namespace DigitalnaForenzikaAdb.Constants
{
    public class CommandPatterns
    {
        public const string ContentQueryPattern = "content query --uri content://{0} --projection {1}";
        public const string LsPattern = "ls {0} -1";
        public const string PullPattern = "pull {0} {1}";
    }
}
