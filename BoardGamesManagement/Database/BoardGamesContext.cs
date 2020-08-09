using BoardGamesManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesManagement.Database
{
    public class BoardGamesContext : DbContext
    {
        public BoardGamesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<GameHistory>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder
                .Entity<GameHistory>()
                .HasOne(p => p.Game)
                .WithMany(p => p.GameHistory)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
    }
}
