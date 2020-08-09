using BoardGamesManagement.Domain;

namespace BoardGamesManagement.Helpers
{
    public interface IHelper<in TEntity, out TEntityDTO> where TEntity : BaseEntity where TEntityDTO : class
    {
        TEntityDTO GetDTO(TEntity entity);
    }
}
