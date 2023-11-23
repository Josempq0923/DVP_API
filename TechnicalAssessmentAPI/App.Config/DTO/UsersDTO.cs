namespace App.Config.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; } 
        public string? Password { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string? Token { get; set; }
    }
}
