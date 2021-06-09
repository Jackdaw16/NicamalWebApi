using System;

namespace NicamalWebApi.Models
{
    public class Report
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PublicationId { get; set; }
        public Publication Publication { get; set; }
        public string ReportedUserId { get; set; }
        public User ReportedUser { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
    }
}