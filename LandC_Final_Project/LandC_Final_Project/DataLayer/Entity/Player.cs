namespace LandC_Final_Project.Entity
{
    public partial class Player
    {
        public Player()
        {
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;

        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
