using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Dtos.Add;
using WebApplication1.Dtos.QuestionDtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Questions")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

        public QuestionsController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<QuestionDto> GetList()
        {
            var Questions = _context.Questions.ToList();
            var QuestionDtos = _mapper.Map<List<QuestionDto>>(Questions);
            return QuestionDtos;
        }
        [HttpGet("{id}")]
        public QuestionDto Get(int id)
        {
            var Question = _context.Questions.FirstOrDefault(x => x.Id == id);
            var QuestionDto = _mapper.Map<QuestionDto>(Question);
            return QuestionDto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public QuestionAddDto QuestionAddDto(QuestionAddDto QuestionAddDto)
        {
            var Question = _mapper.Map<Question>(QuestionAddDto);
            _context.Questions.Add(Question);
            _context.SaveChanges();
            return QuestionAddDto;
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]

        public QuestionDto QuestionUpdateDto(QuestionDto QuestionDto)
        {
            var Question = _mapper.Map<Question>(QuestionDto);
            _context.Questions.Update(Question);
            _context.SaveChanges();
            return QuestionDto;
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public ResultDto Delete(int id)
        {
            var Question = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (Question != null)
            {
                _context.Questions.Remove(Question);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Question Deleted";
            }
            else
            {
                result.Status = false;
                result.Message = "Question Not Found";
            }
            return result;
        }
    }
}
