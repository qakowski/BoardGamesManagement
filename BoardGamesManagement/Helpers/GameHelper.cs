using System.Linq;
using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;

namespace BoardGamesManagement.Helpers
{
    public class GameHelper : IHelper<Game, GameDTO>
    {
        readonly IHelper<GameHistory, GameHistoryDTO> _gameHistoryHelper;
        public GameHelper(IHelper<GameHistory, GameHistoryDTO> gameHistoryHelper)
        {
            _gameHistoryHelper = gameHistoryHelper;
        }
        public GameDTO GetDTO(Game entity)
            => new GameDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                MaxPlayers = entity.MaxPlayers,
                MinPlayers = entity.MinPlayers,
                MinRecommendedAge = entity.MinRecommendedAge,
                GameHistories = entity.GameHistory?.OrderBy(p => p.DisplayDate).Select(p => _gameHistoryHelper.GetDTO(p)).ToList()
            };
    }
}
