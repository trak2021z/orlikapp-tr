using BusinessLayer.Contexts;
using BusinessLayer.Models.Match;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Helpers.Pagination;
using Web.Models.Helpers;
using Web.Models.Match;
using Xunit;

namespace IntegrationTests
{
    public class MatchControllerTests: IntegrationTest
    {
        #region GetPagedList
        [Fact]
        public async Task GetPagedList_ReturnsOneAddedMatchWithProperData()
        {
            // Arrange
            await AuthenticateAsync();
            var createdMatch = await CreateSampleMatch();

            // Act
            var response = await TestClient.GetAsync("api/matches/list");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseList = await response.Content.ReadAsAsync<PagedResult<MatchItem>>();
            responseList.Items.Should().HaveCount(1);
            responseList.RowNumber.Should().Be(1);

            var matchItem = responseList.Items.ElementAt(0);
            matchItem.Id.Should().Be(createdMatch.Id);
            matchItem.Description.Should().Be("Desc");
            matchItem.StartDate.Should().Be("2021.06.28 15:00");
            matchItem.EndOfJoiningDate.Should().Be("2021.06.28 12:00");
            matchItem.Minutes.Should().Be(60);
            matchItem.WantedPlayersAmmount.Should().Be(3);
            matchItem.WantedPlayersLeftAmmonut.Should().Be(3);
            matchItem.PlayersAmmount.Should().Be(10);
            matchItem.Founder.Id.Should().Be(CurrentUser.Id);
            matchItem.Field.Id.Should().Be(1);
            matchItem.MatchMembers.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetPagedList_ReturnsProperlyPagedData_WhenFirstPageSelectedAndMoreItemsThanPageSizeExist()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatchList();

            // Act
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("page", "1");
            queryString.Add("size", "5");

            var response = await TestClient.GetAsync($"api/matches/list?{queryString}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseList = await response.Content.ReadAsAsync<PagedResult<MatchItem>>();
            responseList.Items.Should().HaveCount(5);
            responseList.RowNumber.Should().Be(6);
        }

        [Fact]
        public async Task GetPagedList_ReturnsProperlyPagedData_WhenSecondPageSelectedAndMoreItemsThanPageSizeExist()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatchList();

            // Act
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("page", "2");
            queryString.Add("size", "5");

            var response = await TestClient.GetAsync($"api/matches/list?{queryString}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseList = await response.Content.ReadAsAsync<PagedResult<MatchItem>>();
            responseList.Items.Should().HaveCount(1);
            responseList.RowNumber.Should().Be(6);
        }

        [Fact]
        public async Task GetPagedList_ReturnsEmptyList_WhenFilteringByUnconfirmedMatches()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatchList();

            // Act
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("onlyUnconfirmed", "true");

            var response = await TestClient.GetAsync($"api/matches/list?{queryString}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseList = await response.Content.ReadAsAsync<PagedResult<MatchItem>>();
            responseList.Items.Should().HaveCount(0);
            responseList.RowNumber.Should().Be(0);
        }
        #endregion

        #region Create
        [Fact]
        public async Task Create_ReturnsInvalidStartDateError_WhenFieldIsClosedDuringMatchAllMatchTime()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatch();

            // Act
            var response = await TestClient.PostAsJsonAsync($"api/matches", new MatchRequest
            {
                Description = "",
                StartDate = new DateTime(2021, 6, 25, 15, 0, 0),
                EndOfJoiningDate = new DateTime(2021, 6, 24, 12, 0, 0),
                Minutes = 60,
                FieldId = 1,
                PlayersAmmount = 10,
                WantedPlayersAmmount = 3
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsAsync<BadRequestModel>();
            responseContent.ErrorCode.Should().Be((int)MatchError.InvalidStartDate);
        }

        [Fact(Skip = "Business logic to fix")]
        public async Task Create_ReturnsInvalidStartDateError_WhenFieldIsClosedDuringMatchPartOfMatchTime()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatch();

            // Act
            var response = await TestClient.PostAsJsonAsync($"api/matches", new MatchRequest
            {
                Description = "",
                StartDate = new DateTime(2021, 6, 28, 21, 0, 0),
                EndOfJoiningDate = new DateTime(2021, 6, 27, 12, 0, 0),
                Minutes = 90,
                FieldId = 1,
                PlayersAmmount = 10,
                WantedPlayersAmmount = 3
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsAsync<BadRequestModel>();
            responseContent.ErrorCode.Should().Be((int)MatchError.InvalidStartDate);
        }

        [Fact]
        public async Task Create_ReturnsOccupiedFieldError_WhenAnotherMatchIsPlannedDuringSpecifiedTime()
        {
            // Arrange
            await AuthenticateAsync();
            await CreateSampleMatch();

            // Act
            var response = await TestClient.PostAsJsonAsync($"api/matches", new MatchRequest
            {
                Description = "",
                StartDate = new DateTime(2021, 6, 28, 15, 30, 0),
                EndOfJoiningDate = new DateTime(2021, 6, 28, 12, 0, 0),
                Minutes = 60,
                FieldId = 1,
                PlayersAmmount = 10,
                WantedPlayersAmmount = 3
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsAsync<BadRequestModel>();
            responseContent.ErrorCode.Should().Be((int)MatchError.OccupiedField);
        }
        #endregion
    }
}
