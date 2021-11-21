using Commons.DTO;

namespace Commons.Contracts
{
    public interface IResultCommandParser
    {
        Result Parse(string result);
    }
}
