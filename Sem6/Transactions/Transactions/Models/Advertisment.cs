namespace Transactions.Models
{
    public class Advertisment
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}
