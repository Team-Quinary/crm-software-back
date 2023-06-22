using crm_software_back.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace crm_software_back.DTOs
{
    public class DTOFeedbackForm
    {
        public int FormId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<DTOQuestion> Questions { get; set; }
    }
}
