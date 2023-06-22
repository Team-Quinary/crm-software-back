using crm_software_back.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_software_back.DTOs
{
    public class DTOQuestion
    {
        public int QuestionId { get; set; }

        public int FormId { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public bool IsRequired { get; set; }

        public List<DTOOption> Options { get; set; }
    }
}
