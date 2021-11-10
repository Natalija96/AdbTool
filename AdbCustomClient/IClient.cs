using Commons.DTO;
using System.Collections.Generic;

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

        public Result ExeGetCalendarCommand();
        public Result ExeGetEventsCommand();
        public Result ExePullCommand(string shellPath, string savePath);
        public Result ExeGetFilesCommand(string fullpath);
    }
}
