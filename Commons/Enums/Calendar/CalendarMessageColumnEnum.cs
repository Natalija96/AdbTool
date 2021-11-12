using System.ComponentModel;

namespace Commons.Enums.Calendar
{
    public enum CalendarMessageColumnEnum
    {
        [Description("_id")]
        Id,

        [Description("ownerAccount")]
        OwnerAccount,

        [Description("calendar_displayName")]
        CalendarDisplayName,

        [Description("name")]
        Name,

        [Description("account_type")]
        AccountType,

        [Description("visible")]
        Visible,

        [Description("sync_events")]
        SyncEvents
    }
}
