using System.Threading.Tasks;
using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Database.Repositories
{
    public interface IGamesHistoryRepository
    {
        Task AddToVisitHistory(GameHistory newestGameHistory);
    }
}
