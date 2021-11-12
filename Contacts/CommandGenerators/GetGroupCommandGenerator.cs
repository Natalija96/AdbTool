using Commons.Constants;
using Commons.Contracts;
using Commons.Enums.Contacts;
using EnumsNET;
using System;
using System.Linq;

namespace Contacts.CommandGenerators
{
    public class GetGroupCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.ContactsGroupsContentProvider;
            var columns = Enum.GetValues(typeof(GroupColumnEnum))
                .Cast<GroupColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
