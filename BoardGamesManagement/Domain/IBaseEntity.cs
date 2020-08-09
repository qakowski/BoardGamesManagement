using System;

namespace BoardGamesManagement.Domain
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
