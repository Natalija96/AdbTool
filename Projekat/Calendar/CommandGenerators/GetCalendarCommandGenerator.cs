using Commons.Constants;
using Commons.Contracts;
using Commons.Enums.Calendar;
using EnumsNET;
using System;
using System.Linq;

namespace Calendar.CommandGenerators
{
    public class GetCalendarCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.CalendarContentProvider;
            var columns = Enum.GetValues(typeof(CalendarMessageColumnEnum))
                .Cast<CalendarMessageColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
