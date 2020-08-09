using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;

namespace BoardGamesManagement.Helpers
{
    public class GameHistoryHelper : IHelper<GameHistory, GameHistoryDTO>
    {
        public GameHistoryDTO GetDTO(GameHistory entity)
            => new GameHistoryDTO
            {
                DisplayDate = entity.DisplayDate,
                Source = entity.Source
            };
    }
}
