using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_API.Models;

namespace Task_API.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudentByID(int id);
        Task<Student> ADDEditStudent(Student student);
        Task<Student> DeleteStudent(int id);
    }
}
