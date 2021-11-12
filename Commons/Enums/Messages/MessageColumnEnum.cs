using System.ComponentModel;

namespace Commons.Enums.Messages
{
    public enum MessageColumnEnum
    {
        [Description("address")]
        Address = 0,

        [Description("date")]
        Date = 1,

        [Description("date_sent")]
        DateSent = 2,

        [Description("seen")]
        Seen = 3,

        [Description("body")]
        Body = 4
    }
}
