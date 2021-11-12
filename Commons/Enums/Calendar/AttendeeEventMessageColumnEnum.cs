using System.ComponentModel;

namespace Commons.Enums.Calendar
{
    public enum AttendeeEventMessageColumnEnum
    {
        //[Description("Title")]
        //Title,

        [Description("eventTimezone")]
        Event_Id,

        [Description("account_type")]
        AccountType,

        [Description("attendeeName")]
        AtendeeName,

        [Description("attendeeEmail")]
        AtendeeEmail,

        [Description("attendeeRelationship")]
        AttendeeRelationship,

        [Description("attendeeStatus")]
        AttendeeStatus,

        [Description("attendeeType")]
        AttendeeType,

        [Description("hasAlarm")]
        HasAlarm,

        [Description("calendar_id")]
        CalendarId,

        [Description("event_id")]
        EventId,

        [Description("allDay")]
        AllDay,

        [Description("duration")]
        Duration,

        [Description("availability")]
        Availability,

        [Description("organizer")]
        Organizer,

        [Description("eventLocation")]
        EventLocation,

        [Description("account_name")]
        AccountName
    }
}
