namespace WebApplication1.Dtos.SurveyDtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string AppUserId { get; set; }
    }
}
