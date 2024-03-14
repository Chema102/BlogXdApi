namespace blocv1.Models.dto
{
    public class PostDto
    {
        public int id {  get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public int? Category { get; set; }
        public bool? IsFeatured { get; set; }
        public int AutorId { get; set;}
    }
}
