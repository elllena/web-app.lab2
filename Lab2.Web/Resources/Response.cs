using System.Net;

namespace Lab2.Web.Resources
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public static Response Successed() => new Response { StatusCode = HttpStatusCode.OK };

        public static Response ValidationFailed(string errorMessage) => new Response { StatusCode = HttpStatusCode.BadRequest, ErrorMessage = errorMessage };

    }
}
