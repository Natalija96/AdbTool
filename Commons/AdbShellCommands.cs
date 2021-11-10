namespace DigitalnaForenzikaAdb
{
    public class AdbShellCommands
    {
        public const string GetInboxMessagesCommand = "content query --uri content://sms/inbox " +
            "--projection {}:date:date_sent:seen:body";

       // public static string GetInboxMessag3esCommand = $"content query --uri {0} --projection {1}:{2}:{3}:{4}:{5}";
    }
}
