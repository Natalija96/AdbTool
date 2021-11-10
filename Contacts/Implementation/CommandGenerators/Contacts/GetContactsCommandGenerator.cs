using AdbTool.Enums.Contacts;
using Commons;
using DigitalnaForenzikaAdb.Constants;
using EnumsNET;
using System;
using System.Linq;

namespace Contacts
{
    public class GetContactsCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.ContactsDataContentProvider;
            var columns = Enum.GetValues(typeof(ContactGroupColumnEnum))
                .Cast<ContactGroupColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
