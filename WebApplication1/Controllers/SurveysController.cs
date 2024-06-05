using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Dtos.CategoryDtos;
using WebApplication1.Dtos.QuestionDtos;
using WebApplication1.Dtos.SurveyDtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [Route("api/Surveys")]
    [ApiController]
    [Authorize]
    public class SurveysController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

        public SurveysController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        [HttpGet]
        public List<SurveyDto> GetList()
        {
            var Surveys = _context.Surveys.ToList();
            var SurveyDtos = _mapper.Map<List<SurveyDto>>(Surveys);
            return SurveyDtos;
        }
        
        [HttpGet("GetSurveyListAllDetail/{surveyId}")]
        public IActionResult GetSurveyWithQuestions(int surveyId)
        {
 
            var survey = _context.Surveys.Include(s => s.Category)
                .Include(s => s.AppUser)
                .Include(s => s.SurveyQuestions)
                .ThenInclude(sq => sq.Question)
                .FirstOrDefault(s => s.Id == surveyId);

      
            if (survey == null)
            {
                return NotFound("Anket bulunamadı.");
            }

   
            var surveyDto = new GetSurveyListAllDetail
            {
                Id = survey.Id,
                Name = survey.Name,
                Description = survey.Description,
                Category = _mapper.Map<CategoryDto>(survey.Category),
                AppUser = _mapper.Map<UserDto>(survey.AppUser)
            };

           
            surveyDto.Questions = survey.SurveyQuestions
                .Select(sq => _mapper.Map<QuestionDto>(sq.Question))
                .ToArray();

            return Ok(surveyDto);
        }

        [HttpGet("GetSurveyListAllDetail")]
        public IActionResult GetSurveyWithQuestions()
        {
            // Tüm anketleri, ilgili kategorileri, kullanıcıları ve soruları içeren veritabanından alıyoruz
            var surveys = _context.Surveys
                .Include(s => s.Category)
                .Include(s => s.AppUser)
                .Include(s => s.SurveyQuestions)
                .ThenInclude(sq => sq.Question)
                .ToList();

            if (surveys == null || surveys.Count == 0)
            {
                return NotFound("Anket bulunamadı.");
            }

            // Tüm anketleri DTO'lara dönüştürmek için bir liste oluşturuyoruz
            var surveyDtoList = surveys.Select(survey => new GetSurveyListAllDetail
            {
                Id = survey.Id,
                Name = survey.Name,
                Description = survey.Description,
                Category = _mapper.Map<CategoryDto>(survey.Category),
                AppUser = _mapper.Map<UserDto>(survey.AppUser),
                Questions = survey.SurveyQuestions
                    .Select(sq => _mapper.Map<QuestionDto>(sq.Question))
                    .ToArray()
            }).ToList();

            // DTO listesini döndürüyoruz
            return Ok(surveyDtoList);
        }
        [HttpGet("{id}")]
        public SurveyDto Get(int id)
        {
            var Survey = _context.Surveys.FirstOrDefault(x => x.Id == id);
            var SurveyDto = _mapper.Map<SurveyDto>(Survey);
            return SurveyDto;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public SurveyAddDto SurveyAddDto(SurveyAddDto SurveyAddDto)
        {
            var Survey = _mapper.Map<Survey>(SurveyAddDto);
            _context.Surveys.Add(Survey);
            _context.SaveChanges();
            return SurveyAddDto;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddedQuestionSurvey")]
        public IActionResult AddQuestionToSurvey(SurveyAndQuestionBridgeDTO surveyAndQuestionBridgeDTO)
        {
            // Önce, kullanıcının eklemeye çalıştığı survey ve question'ı alıyoruz.
            int surveyId = surveyAndQuestionBridgeDTO.SurveyId;
            int questionId = surveyAndQuestionBridgeDTO.QuestionId;

            // Bu survey ve question için zaten bir SurveyAndQuestionBridge var mı kontrol ediyoruz.
            bool relationshipExists = _context.surveyAndQuestionBridges
                .Any(sqb => sqb.SurveyId == surveyId && sqb.QuestionId == questionId);

            if (relationshipExists)
            {
                // Eğer ilişki zaten mevcutsa, kullanıcıya bir hata mesajı döndürüyoruz.
                return BadRequest("Bu soru bu ankete zaten eklenmiştir.");
            }

            // İlişki yoksa, yeni SurveyAndQuestionBridge ekliyoruz.
            var surveyAndQuestionBridge = _mapper.Map<SurveyAndQuestionBridge>(surveyAndQuestionBridgeDTO);
            _context.surveyAndQuestionBridges.Add(surveyAndQuestionBridge);
            _context.SaveChanges();

            // Başarıyla eklendiyse, kullanıcıya eklenen DTO'yu döndürüyoruz.
            return Ok(surveyAndQuestionBridgeDTO);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]

        public SurveyDto SurveyUpdateDto(SurveyDto SurveyDto)
        {
            var Survey = _mapper.Map<Survey>(SurveyDto);
            _context.Surveys.Update(Survey);
            _context.SaveChanges();
            return SurveyDto;
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public ResultDto Delete(int id)
        {
            var Survey = _context.Surveys.FirstOrDefault(x => x.Id == id);
            if (Survey != null)
            {
                _context.Surveys.Remove(Survey);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Survey deleted successfully";
            }
            else
            {
                result.Status = false;
                result.Message = "Survey not found";
            }
            return result;
        }
    }
}
