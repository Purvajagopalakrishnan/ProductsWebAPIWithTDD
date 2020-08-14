using FluentAssertions;
using Newtonsoft.Json;
using ProductsWebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Products.integrationTests
{
    public class ProductsIntegrationTest
    {
        [Fact]
        public async Task Test_Get_All()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Test_Post()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/products", new StringContent(JsonConvert.SerializeObject(new Product() { Name = "Shirt", Description = "Clothes", Price = 5.00M, Quantity = 2 }), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Test_Get_By_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/3");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Test_Delete_By_Id()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/products/3");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Test_Put()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync("/api/products/5", new StringContent(JsonConvert.SerializeObject(new Product() { Id = "5", Name = "Shirt", Description = "Clothes", Price = 10.00M, Quantity = 3 }), Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
