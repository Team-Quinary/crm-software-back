using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crm_software_back.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, Column(TypeName = "nvarchar(20)")]
        public string Type { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(30)")]
        public string Username { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(30)")]
        public string? Password { get; set; }
        //public string Password { get; set; } = string.Empty;

        //[Required, Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(30)")]
        public string LastName { get; set; } = string.Empty;

        [Required, Column(TypeName = "nvarchar(15)")]
        public string ContactNo { get; set; } = string.Empty;

        [Required, Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(30)")]
        public string ProfilePic { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime ResetPasswordExpiry { get; set; }
    }
}

