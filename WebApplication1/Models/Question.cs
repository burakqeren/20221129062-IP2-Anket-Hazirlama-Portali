namespace WebApplication1.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        //survery
        public List<SurveyAndQuestionBridge> QuestionSurveys { get; set; }


    }
}
