using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ADHDPlanner_Backend_Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public async Task GetAllTasksIntegrationTest()
        {
            // No more boilerplate needed with top level statements (https://docs.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates)
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10000);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://robinvanhoof.tech:1001/api/Task");
            //var content = new StringContent("", null, "text/plain");
            //request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetTaskIntegrationTest()
        {
            // No more boilerplate needed with top level statements (https://docs.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates)
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10000);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://robinvanhoof.tech:1001/api/Task/2");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task UpdateTaskIntegrationTest()
        {
            string[] names = { "Besiege Rome", "Invade Poland", "Curbstomp kids", "Work on school", "Do dishes" };
            string name = names[new Random().Next(0, names.Length)];

            // No more boilerplate needed with top level statements (https://docs.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates)
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10000);
            var request = new HttpRequestMessage(HttpMethod.Put, "http://robinvanhoof.tech:1001/api/Task/2");
            var content = new StringContent("{\r\n    \"id\": 3,\r\n    \"name\": \"" + name  + "\",\r\n    \"isComplete\": false,\r\n    \"duration\": 10,\r\n    \"description\": \"Vacuum the livingroom\",\r\n    \"dueDate\": \"2023-05-15T12:38:20.103Z\",\r\n    \"subtasks\": [\r\n        null\r\n    ]\r\n}", null, "application/json");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task PostTaskIntegrationTest()
        {
            // No more boilerplate needed with top level statements (https://docs.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates)
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10000);
            var request = new HttpRequestMessage(HttpMethod.Post, "http://robinvanhoof.tech:1001/api/Task");
            var content = new StringContent("{\n  \"id\": 6,\n  \"name\": \"Dishes\",\n  \"isComplete\": false,\n  \"duration\": 30,\n  \"description\": \"Do the dishes and put them away\",\n  \"dueDate\": \"2023-05-10T12:38:20.103Z\",\n  \"subtasks\": [null]\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
