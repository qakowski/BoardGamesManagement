using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGamesManagement.Controllers;
using BoardGamesManagement.Database.UnitsOfWork;
using BoardGamesManagement.Domain;
using BoardGamesManagement.Helpers;
using BoardGamesManagement.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BoardGamesManagementTests.Controllers
{
    public class GamesAPITest
    {
        readonly List<Game> gamesList = new List<Game>()
        {
           new Game()
           {
               Id = Guid.NewGuid(),
               MaxPlayers = 4,
               MinPlayers = 2,
               GameHistory = null,
               Name = "Game",
               MinRecommendedAge = 12
           },
           new Game()
           {
               Id = Guid.NewGuid(),
               MaxPlayers = 4,
               MinPlayers = 2,
               GameHistory = null,
               Name = "Game",
               MinRecommendedAge = 12
           }
        };

        [Test]
        public async Task GamesAPI_GetGames_Returns200OK()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.GamesRepository.GetGamesAsync(It.IsAny<int?>())).ReturnsAsync(gamesList);

            var gamesApiController = new GamesAPI(unitOfWorkMock.Object, new GameHelper(new GameHistoryHelper()));

            var result = await gamesApiController.GetGames(new GetGamesRequest() { MaxResults = null });

            Assert.IsInstanceOf(typeof(OkObjectResult), result.Result);
        }

        [Test]
        public async Task GamesAPI_GetGames_Returns404NotFound()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.GamesRepository.GetGamesAsync(It.IsAny<int?>())).ReturnsAsync(new List<Game>());

            var gamesApiController = new GamesAPI(unitOfWorkMock.Object, new GameHelper(new GameHistoryHelper()));

            var result = await gamesApiController.GetGames(new GetGamesRequest() { MaxResults = null });

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }
    }
}
