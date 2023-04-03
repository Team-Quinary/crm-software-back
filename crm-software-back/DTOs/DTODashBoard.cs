namespace crm_software_back.DTOs
{
    public class DTODashBoard
    {
        public int ProjectCount { get; set; }

        public int CustomerCount { get; set; }

        public int TechLeadCount { get; set; }

        public int Completed { get; set; }

        public int Ongoing { get; set; }
        
        public int Suspended { get; set;}

        public List<String>? LastDays { get; set; }

        public List<int>? NewProjects { get; set; }

        public List<double>? Payments { get; set; }
    }
}
