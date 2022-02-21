using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmployeeAPI.Models;
using EmployeeAPI.Wrappers;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet("GetAllDepartments")]
        public JsonResult Get()
        {
            var query = @"
                    select DepartmentId, DepartmentName from dbo.Departments";
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
        
        [HttpGet("GetDepartmentById/{id}")]
        public JsonResult GetById(long id)
        {
            var query = @"
                    select * from dbo.Departments
                    where DepartmentId = " + id + @" 
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
            return new JsonResult(table);
        }

        [HttpPost("AddDepartment")]
        public JsonResult Post(Department dep)
        {
            var query = @"
                    insert into dbo.Departments values 
                    ('" + dep.DepartmentName + @"')
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


        [HttpPut("UpdateDepartment")]
        public JsonResult Put(Department dep)
        {
            var query = @"
                    update dbo.Departments set 
                    DepartmentName = '" + dep.DepartmentName + @"'
                    where DepartmentId = " + dep.DepartmentId + @" 
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


        [HttpDelete("DeleteDepartment/{id}")]
        public JsonResult Delete(long id)
        {
            var query = @"
                    delete from dbo.Departments
                    where DepartmentId = " + id + @" 
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

            return new JsonResult("Deleted Successfully");
        }
    }
}