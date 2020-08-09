using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Database.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly IRepository<Game> _gamesRepository;

        public GamesRepository(IRepository<Game> repository)
        {
            _gamesRepository = repository;
        }

        public async Task<Game> AddAsync(Game game)
        {
            return await _gamesRepository.AddItem(game);
        }

        public async Task DeleteAsync(Guid gameId)
        {
            await _gamesRepository.DeleteItem(gameId);
        }

        public async Task<Game> GetGameByIdAsync(Guid gameId)
        {
            return await _gamesRepository.GetItem(gameId);
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(int? gamesCount)
        {
            return await _gamesRepository.GetItems(gamesCount);
        }

        public Game UpdateAsync(Game game)
        {
            return _gamesRepository.UpdateItem(game);
        }
    }
}
