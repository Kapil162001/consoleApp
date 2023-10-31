namespace LandC_Final_Project.Entity
{
    public partial class Team
    {
        public Team()
        {
            MatchFirstTeams = new HashSet<Match>();
            MatchSecondTeams = new HashSet<Match>();
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int TeamId { get; set; }
        public string Name { get; set; } = null!;
        public int EventId { get; set; }
        public int GameId { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual GameType Game { get; set; } = null!;
        public virtual ICollection<Match> MatchFirstTeams { get; set; }
        public virtual ICollection<Match> MatchSecondTeams { get; set; }
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
