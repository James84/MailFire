using System;

namespace MailFire.DTOs
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public DateTime DateSent { get; set; }
        public string Body { get; set; }
    }
}