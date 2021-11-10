using Calendar;
using Commons;
using Commons.DTO;
using Contacts;
using FileSystem;
using Messages;
using System.Collections.Generic;

namespace AdbCustomClient
{
    public class Client : IClient
    {
        public Result ExeGetAllMessagesCommand()
        {
            var result = new CommandExecutor(new GetInboxMessagesCommandResultParser(),
             new GetAllMessagesCommadGenerator())
              .ExeCommand();

            return result;
        }

        public Result ExeGetInboxMessagesCommand()
        {
            var result = new CommandExecutor(new GetInboxMessagesCommandResultParser(),
               new GetInboxMessagesCommandGenerator())
                .ExeCommand();

            return result;
        }

        public Result ExeGetContactsCommand()
        {
            var result = new CommandExecutor(new GetConcactsCommandResultParser(),
             new GetContactsCommandGenerator())
              .ExeCommand();

            return result;
        }

        public Result ExeGetEventsCommand()
        {
            var result = new CommandExecutor(new GetEventsMessagesCommandResultParser(),
                new GetEventsCommandGenerator())
                .ExeCommand();

            return result;
        }

        public Result ExePullCommand(string shellPath, string savePath)
        {
            var result = new PullCommandExecutor(
                 new PullFileCommandGenerator(shellPath, savePath))
                 .ExeCommand();

            return result;
        }

        public Result ExeGetFilesCommand(string fullpath)
        {
            var result = new CommandExecutor(new GetFilesCommandResultParser(),
                new GetFilesCommandGenerator(fullpath))
                 .ExeCommand();

            return result;
        }

        public Result ExeGetCalendarCommand()
        {
            var result = new CommandExecutor(new GetCalendarMessagesCommandResultParser(),
                new GetCalendarCommandGenerator())
                .ExeCommand();

            return result;
        }

        public Result ExeGetGroupsCommand()
        {

            throw new System.NotImplementedException();
        }

        public Result ExeGetPeopleContactsCommand()
        {
            throw new System.NotImplementedException();
        }

        public Result ExeGetPhoneContactsCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
