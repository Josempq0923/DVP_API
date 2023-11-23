using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Infrastructure.Database.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
