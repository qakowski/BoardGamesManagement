using System.Threading.Tasks;
using BoardGamesManagement.Database.Repositories;

namespace BoardGamesManagement.Database.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IGamesRepository GamesRepository { get; }
        IGamesHistoryRepository GamesHistoryRepository { get; }

        Task<int> Save();
    }
}
