﻿namespace Collab_Project.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }

    }
}
