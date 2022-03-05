using EmployeeAPI.Filter;
using EmployeeAPI.Models;
using EmployeeAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Repositories
{
    public interface IEmployeeRepository
    {
        public JsonResult Get();
        public PagedResponse<JsonResult> GetAll(PaginationFilter filter, string? route);
        public JsonResult GetById(long id);
        public JsonResult Post(Employee dep);
        public JsonResult Put(Employee dep);
        public JsonResult Delete(long id);
    }
}