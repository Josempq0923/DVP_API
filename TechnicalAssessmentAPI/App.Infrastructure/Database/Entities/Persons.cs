using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Infrastructure.Database.Entities
{
    [Table("Persons")]
    public class Persons 
    {    
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string? TypeIdentification { get; set; }
        public string? FullIdentification { get; set; }
    }
}
