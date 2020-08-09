using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesManagement.Domain
{
    [Table("GameHistories")]
    public class GameHistory : BaseEntity
    {

        [ForeignKey("Games")]
        public Guid GameId { get; set; }
        public DateTime DisplayDate { get; set; }
        public string Source { get; set; }
        public virtual Game Game { get; set; }
    }
}
