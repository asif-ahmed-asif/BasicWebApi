using System.ComponentModel.DataAnnotations;

namespace BasicWebApi.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Mobile { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
