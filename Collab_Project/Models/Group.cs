﻿namespace Collab_Project.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
