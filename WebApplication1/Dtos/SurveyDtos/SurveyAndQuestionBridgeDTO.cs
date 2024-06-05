using WebApplication1.Dtos.QuestionDtos;

namespace WebApplication1.Dtos.SurveyDtos
{
    public class SurveyAndQuestionBridgeDTO
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }

        // Gerektiğinde, ilişkili nesnelerin DTO'larını da ekleyebilirsiniz.
        //public SurveyDTO Survey { get; set; }
        //public QuestionDTO Question { get; set; }
    }
}
