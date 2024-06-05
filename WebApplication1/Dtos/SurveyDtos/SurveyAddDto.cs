namespace WebApplication1.Dtos.SurveyDtos
{
    public class SurveyAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int QuestionId { get; set; }
        public string AppUserId { get; set; }
    }
}
