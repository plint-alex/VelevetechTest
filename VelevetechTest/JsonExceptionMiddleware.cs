using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VelevetechTest
{
    public class JsonExceptionMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var error = new
            {
                message = ex.Message
            };

            context.Response.ContentType = "application/json";

            string jsonString = JsonConvert.SerializeObject(error);


            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            await context.Response.WriteAsync(jsonString, Encoding.UTF8);
        }
    }
}
