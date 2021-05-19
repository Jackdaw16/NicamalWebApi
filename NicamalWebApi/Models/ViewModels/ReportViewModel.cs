using System;

namespace NicamalWebApi.Models.ViewModels
{
    public class ReportResponse
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public PublicationForReport Publication { get; set; }
        public UserResponse UserReported { get; set; }
        public UserResponse User { get; set; }
    }
}