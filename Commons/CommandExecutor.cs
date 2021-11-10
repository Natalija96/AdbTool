using Commons.DTO;
using SharpAdbClient;
using System.Linq;

namespace Commons
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IResultCommandParser _resultCommandParser;
        private readonly ICommandGenerator _commandGenerator;
        private readonly IAdbClient _adbClient;

        public CommandExecutor(IResultCommandParser resultCommandParser, ICommandGenerator commandGenerator)
        {
            _resultCommandParser = resultCommandParser;
            _commandGenerator = commandGenerator;
            _adbClient = new AdbClient();
        }

        public Result ExeCommand()
        {
            var device = _adbClient.GetDevices()?.FirstOrDefault();

            var command = _commandGenerator.Generate();
            var reciever = new ConsoleOutputReceiver();

            _adbClient.ExecuteRemoteCommand(command,
                device,
                reciever);

            var result = reciever.ToString();

            return _resultCommandParser.Parse(result);
        }
    }
}
