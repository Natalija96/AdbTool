using System.ComponentModel;

namespace AdbTool.Enums.Contacts
{
    public enum ContactGroupColumnEnum
    {
        [Description("account_name")]
        AccountName = 0,

        [Description("data1")]
        Name = 1,

        [Description("contact_account_type")]
        ContactAccountType = 2,

        [Description("account_type")]
        AccountType = 3,
    }
}
