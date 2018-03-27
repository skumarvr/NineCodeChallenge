using NineCodingChallenge.Models.Assessment;
using System.Collections.Generic;

namespace NineCodingChallenge.Business.Assessment
{
    public interface ISerialManager
    {
        /// <summary>
        /// Method to filter Serial Json
        /// </summary>
        /// <param name="serialJson">Serial json</param>
        /// <returns>Response payload with filtered JSON</returns>
        ResponsePayload FilterSerialJson(RequestPayload serialJson);
    }
}
