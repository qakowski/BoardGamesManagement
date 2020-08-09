using System;
using System.Collections.Generic;

namespace BoardGamesManagement.DTO
{
    public class GameDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int MinRecommendedAge { get; set; }
        public List<GameHistoryDTO> GameHistories { get; set; }
    }
}
