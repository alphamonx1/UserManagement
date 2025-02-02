using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
