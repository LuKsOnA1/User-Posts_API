﻿namespace EntityLayer.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string CreatedTime { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }


        public Comment()
        {
            CreatedTime = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
