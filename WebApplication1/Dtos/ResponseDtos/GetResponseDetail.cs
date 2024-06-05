using WebApplication1.Dtos.QuestionDtos;

namespace WebApplication1.Dtos.ResponseDtos
{
    public class GetResponseDetail
    {
        public string Text { get; set; }

        public QuestionDto Question { get; set; }
        public UserDto User { get; set; }

    }
}
