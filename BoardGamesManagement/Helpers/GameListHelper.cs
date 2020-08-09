using BoardGamesManagement.Domain;
using BoardGamesManagement.DTO;

namespace BoardGamesManagement.Helpers
{
    public class GameListHelper : IHelper<Game, GamesListDTO>
    {

        public GamesListDTO GetDTO(Game entity)
            => new GamesListDTO
            {
                Name = entity.Name,
                Id = entity.Id
            };
    }
}
