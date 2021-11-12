using Commons.Constants;
using Commons.Contracts;

namespace PhoneInfo.CommandGenerator
{
    public class GetPhoneInfoCommandGenerator : ICommandGenerator
    {
        public string Generate()
        {
            return CommandPatterns.DevicePattern;
        }
    }
}
