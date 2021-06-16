using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Models.Role;
using IntegrationTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web;
using Web.Models.Auth;
using Web.Models.Match;

namespace IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected CurrentUser CurrentUser { get; set; }

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(SRBContext));
                        services.AddDbContext<SRBContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<MatchCreateResponse> CreateSampleMatch()
        {
            var createMatchResponse = await TestClient.PostAsJsonAsync("api/matches", new MatchRequest
            {
                Description = "Desc",
                StartDate = new DateTime(2021, 6, 28, 15, 0, 0),
                EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                Minutes = 60,
                FieldId = 1,
                PlayersAmmount = 10,
                WantedPlayersAmmount = 3
            });
            return await createMatchResponse.Content.ReadAsAsync<MatchCreateResponse>();
        }

        protected async Task CreateSampleMatchList()
        {
            var requests = new List<MatchRequest> 
            {
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 10, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 8, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 12, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 10, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 14, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 16, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 18, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 28, 20, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 1,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 29, 20, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 29, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 2,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                },
                new MatchRequest
                {
                    Description = "",
                    StartDate = new DateTime(2021, 6, 29, 20, 0, 0),
                    EndOfJoiningDate = new DateTime(2021, 6, 29, 12, 0, 0),
                    Minutes = 60,
                    FieldId = 2,
                    PlayersAmmount = 10,
                    WantedPlayersAmmount = 3
                }
            };

            foreach (var request in requests)
            {
                var response = await TestClient.PostAsJsonAsync("api/matches", request);
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task<string> GetJwtAsync()
        {
            var login = "user";
            var password = "userpwd123!";

            var registerResponse = await TestClient.PostAsJsonAsync("api/auth/register", new RegisterRequest
            {
                Email = "user@email.com",
                FirstName = "user",
                LastName = "integration",
                Login = login,
                Password = password,
                RepeatedPassword = password
            });
            registerResponse.EnsureSuccessStatusCode();

            var loginResponse = await TestClient.PostAsJsonAsync("api/auth/login", new LoginRequest
            {
                Login = login,
                Password = password
            });
            loginResponse.EnsureSuccessStatusCode();
            var loginResponseContent = await loginResponse.Content.ReadAsAsync<LoginResponse>();
            CurrentUser = new CurrentUser(loginResponseContent);

            return loginResponseContent.Token;
        }
    }
}
