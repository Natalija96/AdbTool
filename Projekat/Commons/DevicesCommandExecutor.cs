using Commons.Contracts;
using Commons.DTO;
using System.Collections.Generic;

namespace Commons
{
    public class DevicesCommandExecutor : ICommandExecutor
    {
        private readonly IResultCommandParser _resultCommandParser;
        private readonly ICommandGenerator _commandGenerator;
        private readonly CustomAdbClient _adbClient;

        public DevicesCommandExecutor(ICommandGenerator commandGenerator, IResultCommandParser resultCommandParser)
        {
            _commandGenerator = commandGenerator;
            _resultCommandParser = resultCommandParser;
            _adbClient = new CustomAdbClient(@"C:\platform-tools\adb.exe");
        }

        public Result ExeCommand()
        {
            var command = _commandGenerator.Generate();
       
            var standardOutput = new List<string>();
            var errorOutput = new List<string>();

            _adbClient.RunAdbCommand(command,
                errorOutput,
                standardOutput);

            return _resultCommandParser.Parse(string.Join("\n",standardOutput));
        }
    }
}
