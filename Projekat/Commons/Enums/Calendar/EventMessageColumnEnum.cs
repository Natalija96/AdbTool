using System.ComponentModel;

namespace Commons.Enums.Calendar
{
    public enum EventMessageColumnEnum
    {
        [Description("_id")]
        Id,

        [Description("eventTimezone")]
        EventTimeZone,

        [Description("title")]
        Title,

        [Description("ownerAccount")]
        OwnerAccount,

        [Description("hasAlarm")]
        HasAlarm,

        [Description("calendar_id")]
        CalendarId,

        [Description("organizer")]
        Organizer,

        [Description("eventLocation")]
        EventLocation,

        [Description("allDay")]
        AllDay,

        [Description("duration")]
        Duration,

        [Description("availability")]
        Availability,
    }
}
