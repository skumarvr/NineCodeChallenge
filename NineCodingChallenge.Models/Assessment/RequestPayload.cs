using System.Collections.Generic;

namespace NineCodingChallenge.Models.Assessment
{
    public class RequestPayload
    {
        public List<Serial> Payload { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public int TotalRecords { get; set; }
    }
}
