using EmployeeAPI.Filter;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using EmployeeAPI.Services;
using EmployeeAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IUriService _uriService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        
        [HttpGet("GetAllEmployees")]
        public JsonResult Get()
        {
            return _employeeRepository.Get();
        }
        
        [HttpGet("GetPaginatedAllEmployees")]
        public PagedResponse<JsonResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return _employeeRepository.GetAll(filter, route);
        }
        
        [HttpGet("GetEmployee/{id}")]
        public JsonResult GetById(long id)
        {
            return _employeeRepository.GetById(id);
        }

        [HttpPost("AddEmployee")]
        public JsonResult Post(Employee emp)
        {
            return _employeeRepository.Post(emp);
        }
        
        [HttpPut("UpdateEmployee")]
        public JsonResult Put(Employee emp)
        {
            return _employeeRepository.Put(emp);
        }
        
        [HttpDelete("DeleteEmployee/{id}")]
        public JsonResult Delete(long id)
        {
            return _employeeRepository.Delete(id);
        }
    }
}