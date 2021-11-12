using Commons.Contracts;
using Commons.DTO;
using Commons.Enums.Contacts;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.CommandResultParsers
{
    public class GetGroupsCommandResultParser : IResultCommandParser
    {
        public Result Parse(string result)
        {
            var rows = result.Split("\nRow:");
            var values = Enum.GetValues(typeof(GroupColumnEnum))
                   .Cast<GroupColumnEnum>()
                   .ToList();

            var parsedRows = new List<List<string>>();

            foreach (var row in rows)
            {
                var valuesStr = values
                    .Select(value => GetValue(value, row))
                    .ToList();

                parsedRows.Add(valuesStr);
            }

            return new Result
            {
                Header = values.Select(value => value.AsString(EnumFormat.Description)),
                Rows = parsedRows
            };
        }

        private string GetValue(GroupColumnEnum value, string row)
        {
            var desc = value.AsString(EnumFormat.Description);

            var nextEnum = (int)value + 1;
            var startIndex = row.IndexOf(desc + "=") + (desc + "=").Length;

            var endIndex = row.Length;
            if (Enum.IsDefined(typeof(GroupColumnEnum), nextEnum))
            {
                var nextDesc = ((GroupColumnEnum)nextEnum).AsString(EnumFormat.Description);
                endIndex = row.IndexOf(", " + nextDesc + "=");
            }

            return row.Substring(startIndex, endIndex - startIndex);
        }
    }
}
