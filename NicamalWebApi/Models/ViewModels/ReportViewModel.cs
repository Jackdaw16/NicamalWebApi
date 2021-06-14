using System;
using System.ComponentModel.DataAnnotations;

namespace NicamalWebApi.Models.ViewModels
{
    public class ReportList
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public PublicationForReport Publication { get; set; }
    }

    public class ReportDetail
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public PublicationForReport Publication { get; set; }
        public UserResponse UserReported { get; set; }
        public UserResponse User { get; set; }
    }

    public class ReportCreate
    {
        public string Id { get; set; }
        [Required]
        public string Reason { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public string PublicationId { get; set; }
        public string ReportedUserId { get; set; }
        public string UserId { get; set; }
    }
    
    
}