using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesManagement.Domain
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MinRecommendedAge { get; set; }
        public virtual IEnumerable<GameHistory> GameHistory { get; set; }
    }
}
