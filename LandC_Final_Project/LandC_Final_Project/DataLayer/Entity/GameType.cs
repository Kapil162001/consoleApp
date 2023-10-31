namespace LandC_Final_Project.Entity
{
    public partial class GameType
    {
        public GameType()
        {
            Teams = new HashSet<Team>();
        }

        public int GameTypeId { get; set; }
        public string GameName { get; set; } = null!;
        public int NoOfPlayers { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
