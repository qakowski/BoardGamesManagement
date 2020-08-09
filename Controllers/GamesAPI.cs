using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagement.Database.UnitsOfWork;
using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;
using BoardGamesManagement.Request;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesAPI : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesAPI(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<BoardGamesManagementAPI>
        [HttpGet("game/games-list")]
        public async  Task<ActionResult<IEnumerable<GamesListDTO>>> GetGames([FromQuery] GetGamesRequest request)
        {
            var games = await _unitOfWork.GamesRepository.GetGamesAsync(request.MaxResults);

            if (games.Count() == 0)
                return NotFound();

            var gamesDTO = games.Select(game => new GameDTO
            {
                Id = game.Id,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                MinRecommendedAge = game.MinRecommendedAge,
                Name = game.Name
            });

            return Ok(gamesDTO);
        }

        // GET api/<BoardGamesManagementAPI>/5
        [HttpGet("game/{id}")]
        public async Task<ActionResult<GameDTO>> GetGameDetailsById([FromRoute] Guid id)
        {
            var game = await _unitOfWork.GamesRepository.GetGameByIdAsync(id);

            await _unitOfWork.GamesHistoryRepository.AddToVisitHistory(new GameHistory
            {
                DisplayDate = DateTime.UtcNow,
                GameId = id,
                Source = "API"
            });

            await _unitOfWork.Save();

            var gameDTO = new GameDTO
            {
                Id = game.Id,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                MinRecommendedAge = game.MinRecommendedAge,
                Name = game.Name,
                GameHistories = game.GameHistory.OrderBy(p => p.DisplayDate).Select(p => new GameHistoryDTO
                {
                    Source = p.Source,
                    DisplayDate = p.DisplayDate
                }).ToList()
            };

            if (game == null)
                return NotFound();

            return Ok(gameDTO);
            
        }
    }
}
