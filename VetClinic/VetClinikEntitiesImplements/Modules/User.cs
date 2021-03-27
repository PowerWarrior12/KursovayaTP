using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
