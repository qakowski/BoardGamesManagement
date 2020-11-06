using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesManagement.Domain
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual int MinPlayers { get; set; }
        public virtual int MaxPlayers { get; set; }
        public virtual int MinRecommendedAge { get; set; }
        public virtual IEnumerable<GameHistory> GameHistory { get; protected set; }
    }
}
