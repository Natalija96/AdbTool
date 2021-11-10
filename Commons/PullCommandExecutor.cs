using Commons.DTO;
using System.Collections.Generic;

namespace Commons
{
    public class PullCommandExecutor : ICommandExecutor
    {
        private readonly IResultCommandParser _resultCommandParser;
        private readonly ICommandGenerator _commandGenerator;
        private readonly CustomAdbClient _adbClient;

        public PullCommandExecutor(ICommandGenerator commandGenerator)
        {
            _commandGenerator = commandGenerator;
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

            var list = new List<List<string>>();
            list.Add(standardOutput);
            return new Result 
            { 
                Header = default,
                Rows = list
            };
        }
    }
}
