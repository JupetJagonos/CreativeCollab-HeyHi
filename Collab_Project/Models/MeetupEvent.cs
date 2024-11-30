namespace Collab_Project.Models
{
    public class MeetupEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<YouTubeLink> YouTubeLinks { get; set; } = new List<YouTubeLink>(); 
    }
}
