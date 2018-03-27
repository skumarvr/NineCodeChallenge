using System;
using System.Web.Http;
using NineCodingChallenge.Business.Assessment;
using NineCodingChallenge.Business.Assessment.Impl;
using NineCodingChallenge.Models.Assessment;
using NineCodingChallenge.Web.Utils;

namespace NineCodingChallenge.Web.Controllers
{
    public class SerialController : ApiController
    {
        #region Properties
        ISerialManager _serialManager;
        #endregion

        #region Constructors
        public SerialController(): this(new SerialManager())
        {}

        public SerialController(ISerialManager serialManager)
        {
            _serialManager = serialManager;
        }
        #endregion

        /// <summary>
        /// Post Action method to filter Request json 
        /// </summary>
        /// <param name="requestJson">Request payload json</param>
        /// <returns>Action result with filtered json or bad request</returns>
        [HttpPost]
        public IHttpActionResult FilterJson([FromBody]RequestPayload requestJson)
        {
            var errMsg  = "Could not decode request: JSON parsing failed";

            try {
                // Check if request payload is not null
                if (requestJson != null && requestJson.Payload != null)
                {
                    // Call Manager method to filter request payload json
                    var response = _serialManager.FilterSerialJson(requestJson);

                    // If response is null, return error response
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else {
                        return new CustomBadRequest(errMsg,Request);
                    }
                }
                else
                {
                    // Return Bad Request response if request payload json is null
                    return new CustomBadRequest(errMsg, Request);
                }
            }
            catch (Exception ex) {
                return new CustomBadRequest(errMsg, Request);
            }
        }
    }
}
