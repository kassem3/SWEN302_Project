namespace SimpleJobPortal.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int JobID { get; set; }
        public string JobSeeker { get; set; }
        public string Resume { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
