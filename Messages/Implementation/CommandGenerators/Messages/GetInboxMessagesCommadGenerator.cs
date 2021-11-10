﻿using AdbTool.Enums.Messages;
using Commons;
using DigitalnaForenzikaAdb.Constants;
using EnumsNET;
using System;
using System.Linq;

namespace Messages
{
    public class GetInboxMessagesCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            var contentProvider = ContentProviderConstants.MessageInboxContentProvider;
            var columns = Enum.GetValues(typeof(MessageColumnEnum))
                .Cast<MessageColumnEnum>()
                .Select(e => e.AsString(EnumFormat.Description));

            var joinedColumns = string.Join(":", columns);

            return string.Format(CommandPatterns.ContentQueryPattern, contentProvider, joinedColumns);
        }
    }
}
