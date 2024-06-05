using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dtos;
using WebApplication1.Dtos.ResponseDtos;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Respones")]
    [ApiController]
    [Authorize]
    public class ResponesController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();

     
        public ResponesController(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<ResponseDto> GetList()
        {
           var responses = _context.Responses.ToList();
            var responseDtos = _mapper.Map<List<ResponseDto>>(responses);
            return responseDtos;

        }

        [HttpGet("GetResponseUserDetailAndQuestion")]
        public List<GetResponseDetail> GetAllList()
        {
            var resposnes = _context.Responses.Include(x => x.Question).Include(x => x.AppUser).ToList();
            var responseDtos = _mapper.Map<List<GetResponseDetail>>(resposnes);
            return responseDtos;
        }
        [HttpGet("GetResponsedetailByID/{id}")]
        public GetResponseDetail GetResponsedetailByID(int id)
        {
            var resposnes = _context.Responses.Include(x => x.Question).Include(x => x.AppUser).Where(x=>x.Id == id).FirstOrDefault();
            var responseDtos = _mapper.Map<GetResponseDetail>(resposnes);
            return responseDtos;
        }
        [HttpGet("{id}")]
        public ResponseDto Get(int id)
        {
            var Response = _context.Responses.FirstOrDefault(x => x.Id == id);
            var ResponseDto = _mapper.Map<ResponseDto>(Response);
            return ResponseDto;
        }

        [HttpPost]
        public IActionResult ResponseAddDto(ResponseAddDto ResponseAddDto)
        {

            string appUserId = ResponseAddDto.AppUserId;
            int questionId = ResponseAddDto.QuestionId;


            var existingResponse = _context.Responses
                .FirstOrDefault(r => r.AppUserId == appUserId && r.QuestionId == questionId);

      
            if (existingResponse != null)
            {
                return Conflict(new { Message = "Bu Kullanıcı Daha Öncden yanıt vermiş" });

            }

            var response = _mapper.Map<Response>(ResponseAddDto);
            _context.Responses.Add(response);
            _context.SaveChanges();


            return Ok("Cevap Başarıyla Eklendi");


        }
        [HttpPut]
        public ResponseDto ResponseUpdateDto(ResponseDto ResponseDto)
        {
            var Response = _mapper.Map<Response>(ResponseDto);
            _context.Responses.Update(Response);
            _context.SaveChanges();
            return ResponseDto;
        }
        [HttpDelete("{id}")]
        public ResultDto Delete(int id)
        {
            var Response = _context.Responses.FirstOrDefault(x => x.Id == id);
            if (Response != null)
            {
                _context.Responses.Remove(Response);
                _context.SaveChanges();
                result.Status = true;
                result.Message = "Response deleted successfully";
            }
            else
            {
                result.Status = false;
                result.Message = "Response not found";
            }
            return result;
        }
    }
}
