using Commons.DTO;

namespace AdbCustomClient
{
    public interface IClient
    {
        #region messages 
        public Result ExeGetInboxMessagesCommand();
        public Result ExeGetAllMessagesCommand();
        #endregion

        #region contacts 
        public Result ExeGetPhoneContactsCommand();
        public Result ExeGetContactsCommand();
        public Result ExeGetGroupsCommand();
        public Result ExeGetPeopleContactsCommand();
        #endregion

        #region Calendar

        public Result ExeGetCalendarCommand();
        public Result ExeGetEventsCommand();
        public Result ExeGetAttendeesCommand();

        #endregion

        #region File system
        public Result ExePullCommand(string shellPath, string savePath);
        public Result ExeGetFilesCommand(string fullpath);
        #endregion

        public Result ExeGetPhoneInfoCommand();
    }
}
