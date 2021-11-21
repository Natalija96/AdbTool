using System.ComponentModel;

namespace Commons.Enums.Contacts
{
    public enum GroupColumnEnum
    {
        [Description("account_name")]
        AccountName = 0,

        [Description("group_visible")]
        GroupVisible = 1,

        [Description("title")]
        Title = 2,

        [Description("account_type")]
        AccountType = 3,
    }
}
