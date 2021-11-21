using Commons.Contracts;
using Commons.DTO;
using Commons.Enums.Calendar;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.CommandResultParsers
{
    public class GetAttendeesMessagesCommandResultParser : IResultCommandParser
    {
        public Result Parse(string result)
        {
            var rows = result.Split("\nRow:");
            var values = Enum.GetValues(typeof(AttendeeEventMessageColumnEnum))
                   .Cast<AttendeeEventMessageColumnEnum>()
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

        private string GetValue(AttendeeEventMessageColumnEnum value, string row)
        {
            var desc = value.AsString(EnumFormat.Description);

            var nextEnum = (int)value + 1;
            var startIndex = row.IndexOf(desc + "=") + (desc + "=").Length;

            var endIndex = row.Length;
            if (Enum.IsDefined(typeof(AttendeeEventMessageColumnEnum), nextEnum))
            {
                var nextDesc = ((AttendeeEventMessageColumnEnum)nextEnum).AsString(EnumFormat.Description);
                endIndex = row.IndexOf(", " + nextDesc + "=");
            }

            return row.Substring(startIndex, endIndex - startIndex);
        }
    }
}
