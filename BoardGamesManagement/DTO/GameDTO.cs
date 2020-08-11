using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BoardGamesManagement.DTO
{
    public class GameDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Minimum players")]
        public int MinPlayers { get; set; }
        [DisplayName("Maximum players")]
        public int MaxPlayers { get; set; }
        [DisplayName("Minimal recommended age")]
        public int MinRecommendedAge { get; set; }
        public List<GameHistoryDTO> GameHistories { get; set; }
    }
}
