using crm_software_back.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace crm_software_back.DTOs
{
    public class DTOOption
    {
        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }
    }
}
