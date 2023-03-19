using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace crm_software_back.DTOs
{
    public class DTOUserCustomer
    {
        public int UserId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string ContactNo { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
    }
}
