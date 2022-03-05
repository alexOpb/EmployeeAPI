using System.Data;
using System.Data.SqlClient;
using EmployeeAPI.Controllers;
using EmployeeAPI.Filter;
using EmployeeAPI.Models;
using EmployeeAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentRepository(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }
        
        public JsonResult Get()
        {
            return _departmentRepository.Get();
        }
        
        public JsonResult GetById(long id)
        {
            return _departmentRepository.GetById(id);
        }

        public JsonResult Post(Department dep)
        {
            return _departmentRepository.Post(dep);
        }
        
        public JsonResult Put(Department dep)
        {
            return _departmentRepository.Put(dep);
        }
        
        public JsonResult Delete(long id)
        {
            return _departmentRepository.Delete(id);
        }
    }
}