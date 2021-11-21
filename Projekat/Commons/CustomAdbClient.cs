using SharpAdbClient;
using System.Collections.Generic;

namespace Commons
{
    public class CustomAdbClient : AdbCommandLineClient
    {
        public CustomAdbClient(string adbPath) : base(adbPath)
        {
           
        }

        public void RunAdbCommand(string command, List<string> errorOutput, List<string> standardOutput)
        {
            base.RunAdbProcess(command, errorOutput, standardOutput);
        }
    }
}
