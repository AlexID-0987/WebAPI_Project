namespace WebAPI_Project.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; } = null;
    }
}
