using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }
        
        [HttpGet("GetAllDepartments")]
        public JsonResult Get()
        {
            return _departmentRepository.Get();
        }
        
        [HttpGet("GetDepartmentById/{id}")]
        public JsonResult GetById(long id)
        {
            return _departmentRepository.GetById(id);
        }

        [HttpPost("AddDepartment")]
        public JsonResult Post(Department dep)
        {
            return _departmentRepository.Post(dep);
        }
        
        [HttpPut("UpdateDepartment")]
        public JsonResult Put(Department dep)
        {
            return _departmentRepository.Put(dep);
        }
        
        [HttpDelete("DeleteDepartment/{id}")]
        public JsonResult Delete(long id)
        {
            return _departmentRepository.Delete(id);
        }
    }
}