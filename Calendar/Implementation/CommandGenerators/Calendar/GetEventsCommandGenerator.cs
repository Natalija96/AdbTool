using Commons;
using DigitalnaForenzikaAdb.Constants;
using DigitalnaForenzikaAdb.Enums;
using EnumsNET;
using System;
using System.Linq;

namespace Calendar
{
    public class GetEventsCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.EventsContentProvider;
            var columns = Enum.GetValues(typeof(EventMessageColumnEnum))
                .Cast<EventMessageColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
