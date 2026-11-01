namespace WebApplication1.Contracts
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public string Director { get; set; }
        public int Runtime { get; set; }
    }
}
