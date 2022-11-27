using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudentManagementSystem_test.DAL;
using StudentManagementSystem_test.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_test.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();

        public SubjectController(IConfiguration configuration, ILogger<SubjectController> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Subject>> GetAllSubjects()
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getAllSubjects");
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var subjects = new List<Subject>();
            foreach (DataRow row in table.Rows) 
            {
                subjects.Add
                    (
                        new Subject 
                        {
                            subjectId = Convert.ToInt32(row["subjectId"]),
                            subjectName = Convert.ToString(row["subjectName"])
                        }
                    );
            }
            return subjects;
        }

        [HttpGet]
        public async Task<List<Subject>> GetSubjectsById(int subjectId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getSubjectById");
            command.Parameters.AddWithValue("@subjectId",subjectId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var subjects = new List<Subject>();
            foreach (DataRow row in table.Rows)
            {
                subjects.Add
                    (
                        new Subject
                        {
                            subjectId = Convert.ToInt32(row["subjectId"]),
                            subjectName = Convert.ToString(row["subjectName"])
                        }
                    );
            }
            return subjects;
        }

        [HttpPost]
        public IActionResult InsertSubject(Subject subject) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "insertSubject");
            command.Parameters.AddWithValue("@subjectName", subject.subjectName);
            int result = command.ExecuteNonQuery();
            dbaccess.closeConnection();
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateSubject(Subject subject) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "updateSubject");
            command.Parameters.AddWithValue("@subjectId", subject.subjectId);
            command.Parameters.AddWithValue("@subjectName",subject.subjectName);
            int result = command.ExecuteNonQuery();
            dbaccess.closeConnection();
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

       
        public IActionResult DeleteSubject(int subjectId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deleteSubject");
            command.Parameters.AddWithValue("@subjectId", subjectId);
            int result = command.ExecuteNonQuery();
            dbaccess.closeConnection();
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
