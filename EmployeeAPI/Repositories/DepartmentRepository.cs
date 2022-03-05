using System.Data;
using System.Data.SqlClient;
using EmployeeAPI.Controllers;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentRepository(ILogger<DepartmentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        
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