using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models.DataModels
{
    public class Chapter : BaseEntity
    {
        [Required]
        public string List = string.Empty;
        [Required]
        public int CourseID { get; set; }
        [Required]
        public virtual Course Course { get; set; } = new Course();
    }
}

