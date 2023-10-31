namespace LandC_Final_Project.Entity
{
    public partial class TeamPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        public virtual Player Player { get; set; } = null!;
        public virtual Team Team { get; set; } = null!;
    }
}
