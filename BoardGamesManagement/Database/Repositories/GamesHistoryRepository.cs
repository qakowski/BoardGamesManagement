using System.Linq;
using System.Threading.Tasks;
using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Database.Repositories
{
    public class GamesHistoryRepository : IGamesHistoryRepository
    {
        private readonly IRepository<GameHistory> _gamesHistoryRepository;

        public GamesHistoryRepository(IRepository<GameHistory> repository)
        {
            _gamesHistoryRepository = repository;
        }
        public async Task AddToVisitHistory(GameHistory newestGameHistory)
        {
            var newAdded = await _gamesHistoryRepository.AddItem(newestGameHistory);

            var gamesHistory = await _gamesHistoryRepository.GetItems(p => p.GameId == newestGameHistory.GameId);
            if (gamesHistory.Count() >= 10)
            {
                var oldestVisited = gamesHistory.OrderBy(p => p.DisplayDate).FirstOrDefault();

                if (oldestVisited != null)
                    await _gamesHistoryRepository.DeleteItem(oldestVisited.Id);
            }
        }
    }
}
