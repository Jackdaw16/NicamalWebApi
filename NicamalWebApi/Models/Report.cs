using System;

namespace NicamalWebApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
        public int ReportedUserId { get; set; }
        public User ReportedUser { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}