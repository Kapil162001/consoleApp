using LandC_Final_Project.Entity;
using Microsoft.EntityFrameworkCore;

namespace LandC_Final_Project.DataLayer.DataContext
{
    public partial class LC_WorkshopContext : DbContext
    {
        public LC_WorkshopContext()
        {
        }

        public LC_WorkshopContext(DbContextOptions<LC_WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<GameType> GameTypes { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Occasion> Occasions { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ITT-KAPILG;Initial Catalog=L&C_Workshop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.EventId)
                    .ValueGeneratedNever()
                    .HasColumnName("eventId");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.Property(e => e.Test)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("test");
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.ToTable("GameType");

                entity.Property(e => e.GameTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("gameTypeId");

                entity.Property(e => e.GameName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("gameName");

                entity.Property(e => e.NoOfPlayers).HasColumnName("noOfPlayers");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match");

                entity.Property(e => e.MatchId)
                    .ValueGeneratedNever()
                    .HasColumnName("matchId");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.FirstTeamId).HasColumnName("firstTeamId");

                entity.Property(e => e.SecondTeamId).HasColumnName("secondTeamId");

                entity.HasOne(d => d.FirstTeam)
                    .WithMany(p => p.MatchFirstTeams)
                    .HasForeignKey(d => d.FirstTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Match__firstTeam__300424B4");

                entity.HasOne(d => d.SecondTeam)
                    .WithMany(p => p.MatchSecondTeams)
                    .HasForeignKey(d => d.SecondTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("secondTeamId");
            });

            modelBuilder.Entity<Occasion>(entity =>
            {
                entity.ToTable("Occasion");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.PlayerId)
                    .ValueGeneratedNever()
                    .HasColumnName("playerId");

                entity.Property(e => e.PlayerName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("playerName");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.TeamId)
                    .ValueGeneratedNever()
                    .HasColumnName("teamId");

                entity.Property(e => e.EventId).HasColumnName("eventId");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team__eventId__31EC6D26");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team__gameId__32E0915F");
            });

            modelBuilder.Entity<TeamPlayer>(entity =>
            {
                entity.ToTable("Team_Player");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId).HasColumnName("playerId");

                entity.Property(e => e.TeamId).HasColumnName("teamId");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Play__playe__33D4B598");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamPlayers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Play__teamI__34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
