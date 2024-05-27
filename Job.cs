namespace SimpleJobPortal.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Employer { get; set; }
        public string Location { get; set; }
        public DateTime PostedDate { get; set; }
        public string JobType { get; set; } // Full-time, Part-time, Contract
        public decimal Salary { get; set; }
    }
}
