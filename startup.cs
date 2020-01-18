using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace HelloWorld
{
    public class Startup
    {
        private static readonly byte[] _helloWorldBytes = Encoding.UTF8.GetBytes("Hello, World!");

        public void Configure(IApplicationBuilder app)
        {
            app.Run((httpContext) =>
            {
                var response = httpContext.Response;
                response.StatusCode = 200;
                response.ContentType = "text/plain";

                var helloWorld = _helloWorldBytes;
                response.ContentLength = helloWorld.Length;
                return response.Body.WriteAsync(helloWorld, 0, helloWorld.Length);
            });
        }

        public static Task Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5001")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            return host.RunAsync();
        }
    }
}