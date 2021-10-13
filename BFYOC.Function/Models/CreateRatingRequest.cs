namespace BFYOC.Function.Models
{
    public class CreateRatingRequest
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string LocationName { get; set; }
        public int? Rating { get; set; }
        public string UserNotes { get; set; }
    }

}
