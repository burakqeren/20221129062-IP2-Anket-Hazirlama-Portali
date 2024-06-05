using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Dtos.QuestionDtos;
using WebApplication1.Models;

namespace WebApplication1.Dtos.SurveyDtos
{
    public class GetSurveyListAllDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryDto Category { get; set; }

        public UserDto AppUser { get; set; }

        // Anketin sorularını tutmak için bir dizi QuestionDto
        public QuestionDto[] Questions { get; set; }
    }
}
