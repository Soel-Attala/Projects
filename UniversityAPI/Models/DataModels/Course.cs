using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models.DataModels
{

    public enum CourseLevel
    {
        Basico,
        Intermedio,
        Avanzado
    }

    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required, StringLength(600)]
        public string LongDescription { get; set; } = string.Empty;
        [Required, StringLength(20)]
        public string TargetAudience { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Objectives { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Requirements { get; set; } = string.Empty;
        [Required]
        public CourseLevel Level { get; set; }

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
