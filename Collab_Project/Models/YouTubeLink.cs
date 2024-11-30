using System.ComponentModel.DataAnnotations; 


namespace Collab_Project.Models
{
    public class YouTubeLink
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        [Required]
        public int MeetupEventId { get; set; }
        public virtual MeetupEvent? MeetupEvent { get; set; } 
    }
}
