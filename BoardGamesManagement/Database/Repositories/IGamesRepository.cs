using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Database.Repositories
{
    public interface IGamesRepository
    {
        public Task<Game> AddAsync(Game game);
        public Task<IEnumerable<Game>> GetGamesAsync(int? gamesCount);
        public Task<Game> GetGameByIdAsync(Guid gameId);
        public Task DeleteAsync(Guid gameId);
        public Game Update(Game game);
    }
}
