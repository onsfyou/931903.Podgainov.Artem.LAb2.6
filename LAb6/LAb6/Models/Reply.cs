using System.ComponentModel.DataAnnotations;

namespace LAb6.Models
{
    public class Reply
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }
        public bool IsEdited { get; set; }
        public string AuthorName { get; set; }
        public int ParentTopicId { get; set; }
    }
}
