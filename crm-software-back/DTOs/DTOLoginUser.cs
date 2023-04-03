using Microsoft.AspNetCore.Mvc;

namespace crm_software_back.DTOs
{
    public class DTOLoginUser
    {
        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ExpireDate { get; set; }

        public FileContentResult ProfilePic { get; set; }
    }
}
