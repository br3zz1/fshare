using System.ComponentModel.DataAnnotations;

namespace FShare.Domain
{
    public class User
    {
        [Key]
        public Guid Guid { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime? ExpiresUtc { get; set; }
    }
}
