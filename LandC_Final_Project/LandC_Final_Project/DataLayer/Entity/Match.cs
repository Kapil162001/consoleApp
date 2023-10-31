namespace LandC_Final_Project.Entity
{
    public partial class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }
        public int FirstTeamId { get; set; }
        public int SecondTeamId { get; set; }
        public int Duration { get; set; }

        public virtual Team FirstTeam { get; set; } = null!;
        public virtual Team SecondTeam { get; set; } = null!;
    }
}
