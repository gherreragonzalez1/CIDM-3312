using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace api.Services
{
    public class TextResponseFormatter : IResponseFormatter
    {
        private int responseCounter = 0;
        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Response {++responseCounter}: {content}\n");
        }        
    }
}