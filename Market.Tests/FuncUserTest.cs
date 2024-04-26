using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Market.Tests
{
    public class FuncUserTest
    {
        private const string baseUrl = "http://localhost:5000/api";

        [Test]
        public async Task CreateSameUserShouldConflict()
        {
            using var httpClient = new HttpClient();

            var body = JsonContent.Create(new
            {
                name = DataUserTestGenerator.GetValidUserName(),
                login = DataUserTestGenerator.GetValidLogin(),
                password = DataUserTestGenerator.GetValidPassword()
            });

            await httpClient.PostAsync($"{baseUrl}/users", body);

            var secondCreateResult = await httpClient.PostAsync($"{baseUrl}/users", body);

            Assert.AreEqual(HttpStatusCode.Conflict, secondCreateResult.StatusCode);
        }
    }
}
