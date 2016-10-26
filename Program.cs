using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var host = new WebHostBuilder()
            //     .UseServer(new MyServer())
            //     .UseStartup<Startup>()
            //     .Build();

            // host.Run();

            var client = new TestServer(new WebHostBuilder().UseStartup<Startup>()).CreateClient();
            var text = client.GetStringAsync("http://localhost").Result;
            Console.WriteLine(text);
        }
    }

    public class MyServer : IServer
    {
        public IFeatureCollection Features {get;}

        public void Dispose()
        { }

        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            var features = new FeatureCollection();
            features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            features.Set<IHttpResponseFeature>(new HttpResponseFeature());

            var context = application.CreateContext(features);
            application.ProcessRequestAsync(context);
        }
    }
}
