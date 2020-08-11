using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesManagement.Domain
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        [DisplayName("Minimum players")]
        public int MinPlayers { get; set; }
        [DisplayName("Maximum players")]
        public int MaxPlayers { get; set; }
        [DisplayName("Minimal recommended age")]
        public int MinRecommendedAge { get; set; }
        public virtual IEnumerable<GameHistory> GameHistory { get; set; }
    }
}
