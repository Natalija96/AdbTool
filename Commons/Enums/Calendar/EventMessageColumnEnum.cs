using System.ComponentModel;

namespace DigitalnaForenzikaAdb.Enums
{
    public enum EventMessageColumnEnum
    {
        [Description("eventTimezone")]
        EventTimeZone,

        ////[Description("title")]
        //Title = 1,

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
    }
}
