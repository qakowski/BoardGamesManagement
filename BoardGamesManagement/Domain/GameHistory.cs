using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesManagement.Domain
{
    [Table("GameHistories")]
    public class GameHistory : BaseEntity
    {

        [ForeignKey("Games")]
        public virtual Guid GameId { get; set; }
        public virtual DateTime DisplayDate { get; set; }
        public virtual string Source { get; set; }
        public virtual Game Game { get; set; }
    }
}
