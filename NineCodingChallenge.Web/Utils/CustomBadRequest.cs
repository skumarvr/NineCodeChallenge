using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace NineCodingChallenge.Web.Utils
{
    public class CustomErrorMessage
    {
        public string error { get; set; }
    }

    public class CustomBadRequest : IHttpActionResult
    {
        CustomErrorMessage _errMsg = new CustomErrorMessage();
        HttpRequestMessage _request;

        public CustomBadRequest(string value, HttpRequestMessage request)
        {
            _errMsg.error = value;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<CustomErrorMessage>(_errMsg, new JsonMediaTypeFormatter()),
                RequestMessage = _request,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
            return Task.FromResult(response);
        }
    }
}