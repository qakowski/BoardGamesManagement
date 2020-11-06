using System.Threading.Tasks;
using BoardGamesManagement.Database.Repositories;

namespace BoardGamesManagement.Database.UnitsOfWork
{
    class UnitOfWork : IUnitOfWork
    {
        public IGamesRepository GamesRepository { get; }

        public IGamesHistoryRepository GamesHistoryRepository { get; }

#if EF
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
#elif NHIB
        public UnitOfWork(IGamesRepository gamesRepository, IGamesHistoryRepository gamesHistoryRepository)
        {
            GamesRepository = gamesRepository;
            GamesHistoryRepository = gamesHistoryRepository;
        }

        public Task<int> Save()
        {
            throw new System.NotImplementedException();
        }
#endif
    }
}
