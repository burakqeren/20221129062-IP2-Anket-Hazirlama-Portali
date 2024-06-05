namespace WebApplication1.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //Question

        public List<SurveyAndQuestionBridge> SurveyQuestions { get; set; }

        //User
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
