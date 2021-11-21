using System.Collections.Generic;

namespace Commons.DTO
{
    public class Result
    {
        public IEnumerable<string> Header { get; set; }
        public IEnumerable<IEnumerable<string>> Rows { get; set; }
    }
}
