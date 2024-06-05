namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Survey> Surveys { get; set;}
    }
}
