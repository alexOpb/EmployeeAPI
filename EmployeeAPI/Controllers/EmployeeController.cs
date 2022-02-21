using System.Data;
using System.Data.SqlClient;
using EmployeeAPI.Filter;
using EmployeeAPI.Helpers;
using EmployeeAPI.Models;
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
        private readonly IConfiguration _configuration;
        private readonly IUriService _uriService;

        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration, IUriService uriService)
        {
            _logger = logger;
            _configuration = configuration;
            _uriService = uriService;
        }
        
        [HttpGet("GetAllEmployees")]
        public JsonResult Get()
        {
            var query = @"
                    select * from dbo.Employees";
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        
        [HttpGet("GetPaginatedAllEmployees")]
        public PagedResponse<JsonResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var offset = (validFilter.PageNumber - 1) * (validFilter.PageSize);
            var rows = validFilter.PageSize;

            var query = @"

            select * from dbo.Employees 
                order by EmployeeId 
            offset " + offset + @" rows
                FETCH NEXT " + rows + @" rows only
                ";
            

            
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            
            query = @"
                    SELECT COUNT(*) FROM dbo.Employees
                ";
            int totalRecords;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    totalRecords = (int) myCommand.ExecuteScalar();
                }
            }

            var data = new JsonResult(table);
            
            var pagedReponse = PaginationHelper.CreatePagedReponse(data, validFilter, totalRecords, _uriService, route);

            return pagedReponse;
        }

        
        [HttpGet("GetEmployeeById/{id}")]
        public JsonResult GetById(long id)
        {
            var query = @"
                    select * from dbo.Employees
                    where EmployeeId = " + id + @" 
                    ";
            var table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost("AddEmployee")]
        public JsonResult Post(Employee emp)
        {
            var query = @"
                        insert into dbo.Employees 
                        (Name,DepartmentId,Title,ReportsTo,HireDate)
                        values 
                        (
                        '" + emp.Name + @"'
                        ,'" + emp.DepartmentId + @"'
                        ,'" + emp.Title + @"'
                        ,'" + emp.ReportsTo + @"'
                        ,'" + emp.HireDate + @"'
                        )
                        ";
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut("UpdateEmployee")]
        public JsonResult Put(Employee emp)
        {
            var query = @"
                        update dbo.Employees set
                        Name = '" + emp.Name + @"'
                        ,DepartmentId = '" + emp.DepartmentId + @"'
                        ,Title = '" + emp.Title + @"'
                        ,ReportsTo = '" + emp.ReportsTo + @"'
                        ,HireDate = '" + emp.HireDate + @"'
                         where EmployeeId = " + emp.EmployeeId + @"
                        ";

            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (var myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("DeleteEmployee/{id}")]
        public JsonResult Delete(long id)
        {
            var query = @"
                    delete from dbo.Employees
                    where EmployeeId = " + id + @" 
                    ";
            var table = new DataTable();
            var sqlDataSource = _configuration.GetConnectionString("DevConnection");
            SqlDataReader myReader;
            using (var myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}