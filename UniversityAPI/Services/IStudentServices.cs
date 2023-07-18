using UniversityAPI.Models.DataModels;

namespace UniversityAPI.Services
{
    public interface IStudentServices
    {
        IEnumerable<Student> GetStudentWithOutCourses();
        IEnumerable<Student> GetStudentWithCourses();

    }
}
