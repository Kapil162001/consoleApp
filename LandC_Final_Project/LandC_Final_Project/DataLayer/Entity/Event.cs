namespace LandC_Final_Project.Entity
{
    public partial class Event
    {
        public Event()
        {
            Teams = new HashSet<Team>();
        }

        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Test { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
