namespace WebApplication1.Models
{
    public class Response
    {
        public int Id { get; set; }
        public string Text { get; set; }


        //question
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        //Appuser   
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
