using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task_API.Models;
using Task_API.Repository;

namespace Task_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = MyCustomTokenAuthOptions.DefaultScemeName)]
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRep;
        protected readonly ILogger _logger;
        public StudentController(IStudentRepository studentRepository, ILoggerFactory logger)
        {
            _studentRep = studentRepository;
            _logger = logger.CreateLogger(typeof(StudentController));
        }

        // GET api/values
        [HttpGet]
        public async Task<List<Student>> GetStudents()
        {
            _logger.LogInformation("Run endpoint GetStudents", "/api/Student", "Get");
            var result= await _studentRep.GetStudents();
            _logger.LogInformation("Run endpoint GetStudents {result}", result);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentByID(int id)
        {
            _logger.LogInformation("Run endpoint GetStudentByID {id}", "/api/Student", "Get",id);
            var result = await _studentRep.GetStudentByID(id);
            _logger.LogInformation("Run endpoint GetStudentByID {result}", result);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddEditCustomer([FromBody] Student student)
        {
            if (student == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid State");
            }

            return await _studentRep.ADDEditStudent(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            _logger.LogInformation("Run endpoint DeleteStudent {id}", "/api/Student", "Get", id);
            var result = await _studentRep.DeleteStudent(id);
            _logger.LogInformation("Run endpoint GetStudentByID {result}", result);
            return result;
        }
    }
}
