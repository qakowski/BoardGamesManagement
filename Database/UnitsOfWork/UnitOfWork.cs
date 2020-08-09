using System.Threading.Tasks;
using BoardGamesManagement.Database.Repositories;

namespace BoardGamesManagement.Database.UnitsOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        public IGamesRepository GamesRepository { get; }

        public IGamesHistoryRepository GamesHistoryRepository { get; }

        private readonly BoardGamesContext _context;

        public UnitOfWork(IGamesRepository gamesRepository, IGamesHistoryRepository gamesHistoryRepository, BoardGamesContext context)
        {
            GamesRepository = gamesRepository;
            GamesHistoryRepository = gamesHistoryRepository;
            _context = context;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
