namespace WebApplication1.Models
{
    public class SurveyAndQuestionBridge
    {
        

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
