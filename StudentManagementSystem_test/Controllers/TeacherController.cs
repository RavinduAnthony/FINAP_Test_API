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
    public class TeacherController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();

        public TeacherController(IConfiguration configuration, ILogger<TeacherController> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Teacher>> GetAllTeachers() 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spTeacherOperations",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getAllTeachers");
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var teachers = new List<Teacher>();
            foreach (DataRow row in table.Rows) 
            {
                teachers.Add
                    (
                        new Teacher 
                        {
                            teacherId = Convert.ToInt32(row["teacherId"]),
                            teacherFirstName = Convert.ToString(row["teacherFirstName"]),
                            teacherLastName = Convert.ToString(row["teacherLastName"]),
                            contactNo = Convert.ToString(row["teacherContactNo"]),
                            email = Convert.ToString(row["teacherEmail"])
                        }
                    );
            }
            return teachers;
        }

        [HttpGet]
        public async Task<List<Teacher>> GetTeacherById(int teacherId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spTeacherOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getTeacherById");
            command.Parameters.AddWithValue("@teacherId",teacherId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var teachers = new List<Teacher>();
            foreach (DataRow row in table.Rows)
            {
                teachers.Add
                    (
                        new Teacher
                        {
                            teacherId = Convert.ToInt32(row["teacherId"]),
                            teacherFirstName = Convert.ToString(row["teacherFirstName"]),
                            teacherLastName = Convert.ToString(row["teacherLastName"]),
                            contactNo = Convert.ToString(row["teacherContactNo"]),
                            email = Convert.ToString(row["teacherEmail"])
                        }
                    );
            }
            return teachers;
        }

        [HttpPost]
        public IActionResult InsertTeacher(Teacher teacher)
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spTeacherOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "insertTeacher");
            command.Parameters.AddWithValue("@teacherId", teacher.teacherId);
            command.Parameters.AddWithValue("@teacherFirstName", teacher.teacherFirstName);
            command.Parameters.AddWithValue("@teacherLastName", teacher.teacherLastName);
            command.Parameters.AddWithValue("@teacherContactNumber", teacher.contactNo);
            command.Parameters.AddWithValue("@teacherEmail", teacher.email);
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
        public IActionResult UpdateTeacher(Teacher teacher) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spTeacherOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "updateTeacher");
            command.Parameters.AddWithValue("@teacherId", teacher.teacherId);
            command.Parameters.AddWithValue("@teacherFirstName", teacher.teacherFirstName);
            command.Parameters.AddWithValue("@teacherLastName", teacher.teacherLastName);
            command.Parameters.AddWithValue("@teacherContactNumber", teacher.contactNo);
            command.Parameters.AddWithValue("@teacherEmail", teacher.email);
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

        [HttpDelete]
        public IActionResult DeleteTeacher(int teacherId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spTeacherOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deleteTeacher");
            command.Parameters.AddWithValue("@teacherId", teacherId);
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
