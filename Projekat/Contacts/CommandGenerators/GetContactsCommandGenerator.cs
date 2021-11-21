using Commons.Constants;
using Commons.Contracts;
using Commons.Enums.Contacts;
using EnumsNET;
using System;
using System.Linq;

namespace Contacts.CommandGenerators
{
    public class GetContactsCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.ContactsDataContentProvider;
            var columns = Enum.GetValues(typeof(ContactColumnEnum))
                .Cast<ContactColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
