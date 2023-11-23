namespace App.Config.DTO
{
    public class PersonsDTO
    {
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
