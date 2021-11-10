using System.ComponentModel;

namespace AdbTool.Enums.Calendar
{
    public enum CalendarMessageColumnEnum
    {

        [Description("ownerAccount")]
        OwnerAccount = 0,

        [Description("calendar_displayName")]
        CalendarDisplayName = 1,

        [Description("name")]
        Name = 2,

        [Description("account_type")]
        AccountType = 3,

        [Description("visible")]
        Visible = 4,

        [Description("sync_events")]
        SyncEvents = 5
    }
}
