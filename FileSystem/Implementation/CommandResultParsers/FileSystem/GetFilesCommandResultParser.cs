using Commons;
using Commons.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FileSystem
{
    public class GetFilesCommandResultParser : IResultCommandParser
    {
        public Result Parse(string result)
        {
            var list = new List<List<string>>();
            
            if (result.Length > 2)
            {
                result = result.Remove(result.Length - 2, 2);
            }

            list.Add(result.Split("\r\n").ToList());

            return new Result { Header = null, Rows = list };
        }
    }
}
