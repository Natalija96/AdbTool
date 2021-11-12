using Commons.Contracts;
using Commons.DTO;
using System.Collections.Generic;

namespace PhoneInfo.CommandResultParsers
{
    public class GetPhoneInfoCommandResultParser : IResultCommandParser
    {
        public Result Parse(string result)
        {
            var rows = result.Split("\n");

            if (rows.Length < 2)
            {
                return null;
            }

            return new Result
            {
                Header = rows
            };
        }
    }
}
