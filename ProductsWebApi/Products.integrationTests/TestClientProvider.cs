using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using ProductsWebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Products.integrationTests
{
    public class TestClientProvider : IDisposable
    {
        public HttpClient Client { get; set; }

        private TestServer server;

        public TestClientProvider()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
