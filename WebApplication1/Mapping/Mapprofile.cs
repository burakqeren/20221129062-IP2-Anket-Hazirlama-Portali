using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Dtos.Add;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Dtos.QuestionDtos;
using WebApplication1.Dtos.ResponseDtos;
using WebApplication1.Dtos.SurveyDtos;
using WebApplication1.Models;

namespace WebApplication1.Mapping
{
    public class Mapprofile : Profile
    {
        public Mapprofile()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionAddDto>().ReverseMap();
            CreateMap<Response, ResponseDto>().ReverseMap();
            CreateMap<Response, ResponseAddDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Survey, SurveyAddDto>().ReverseMap();
            CreateMap<GetResponseDetail, Response>()
                .ForMember(opt=>opt.Question,opt=>opt.MapFrom(Question=>Question.Question))
                .ForMember(opt => opt.AppUser, opt => opt.MapFrom(AppUser => AppUser.User))
                .ReverseMap();
            CreateMap<SurveyAndQuestionBridge, SurveyAndQuestionBridgeDTO>().ReverseMap();
            

        }
    }
}
