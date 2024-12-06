
namespace EventManagerClient.Domain.Entities
{
    public class RequestItem
    {
        public string? RequesterName { get; set; }
        public int RequesterID { get; set; }
        public int RequesterEventCount { get; set; }
        public string? RequesterDesc { get; set; }
    }
}
