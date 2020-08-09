using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagement.Database.UnitsOfWork;
using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;
using BoardGamesManagement.Helpers;
using BoardGamesManagement.Request;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesAPI : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelper<Game, GameDTO> _gameHelper;

        public GamesAPI(IUnitOfWork unitOfWork, 
            IHelper<Game, GameDTO> gameHelper)
        {
            _unitOfWork = unitOfWork;
            _gameHelper = gameHelper;
        }

        /// <summary>
        /// Get games from database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("game/games-list")]
        public async  Task<ActionResult<IEnumerable<GamesListDTO>>> GetGames([FromQuery] GetGamesRequest request)
        {
            var games = await _unitOfWork.GamesRepository.GetGamesAsync(request.MaxResults);

            if (games.Count() == 0)
                return NotFound();

            var gamesDTO = games.Select(game => _gameHelper.GetDTO(game));

            return Ok(gamesDTO);
        }

        /// <summary>
        /// Get game details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("game/{id}")]
        public async Task<ActionResult<GameDTO>> GetGameDetailsById([FromRoute] Guid id)
        {
            var game = await _unitOfWork.GamesRepository.GetGameByIdAsync(id);

            if (game == null)
                return NotFound();

            await _unitOfWork.GamesHistoryRepository.AddToVisitHistory(new GameHistory
            {
                DisplayDate = DateTime.UtcNow,
                GameId = id,
                Source = "API"
            });

            await _unitOfWork.Save();

            var gameDTO = _gameHelper.GetDTO(game);

            return Ok(gameDTO);          
        }
    }
}
