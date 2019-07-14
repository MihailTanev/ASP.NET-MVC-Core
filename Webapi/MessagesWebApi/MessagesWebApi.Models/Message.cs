namespace MessagesWebApi.Models
{
    using System;

    public class Message
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
