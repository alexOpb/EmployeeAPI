using EmployeeAPI.Filter;
using EmployeeAPI.Models;
using EmployeeAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Repositories
{
    public interface IDepartmentRepository
    {
        public JsonResult Get();
        public JsonResult GetById(long id);
        public JsonResult Post(Department emp);
        public JsonResult Put(Department emp);
        public JsonResult Delete(long id);
    }
}