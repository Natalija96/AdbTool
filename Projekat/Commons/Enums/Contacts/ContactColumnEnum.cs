using System.ComponentModel;

namespace Commons.Enums.Contacts
{
    public enum ContactColumnEnum
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
