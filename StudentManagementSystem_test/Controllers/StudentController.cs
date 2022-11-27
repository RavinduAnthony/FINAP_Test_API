using Microsoft.AspNetCore.Cors;
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
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();

        public StudentController(IConfiguration configuration, ILogger<StudentController> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        public async Task<List<Student>> GetAllStudents()
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spStudentOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getAllStudents");
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); }); 
            dbaccess.closeConnection();
            var students = new List<Student>();
            foreach (DataRow row in table.Rows) 
            {
                students.Add
                    (
                        new Student 
                        {
                            studentId = Convert.ToInt32(row["studentId"]),
                            firstName = Convert.ToString(row["firstName"]),
                            lastName = Convert.ToString(row["lastName"]),
                            contactPerson = Convert.ToString(row["contactPerson"]),
                            contactNo = Convert.ToString(row["contactNo"]),
                            email = Convert.ToString(row["email"]),
                            dateOfBirth = Convert.ToDateTime(row["dob"]),
                            age = Convert.ToInt32(row["age"]),
                            st_classRoomId = Convert.ToInt32(row["st_classRoomId"])
                        }
                    
                    );
            }
            return students;
        }

        [HttpGet]
        public async Task<List<Student>> GetStudentById(int studentId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spStudentOperations",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getStudentById");
            command.Parameters.AddWithValue("@studentId",studentId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var student = new List<Student>();
            foreach (DataRow row in table.Rows) 
            {
                student.Add
                    (
                        new Student
                        {
                            studentId = Convert.ToInt32(row["studentId"]),
                            firstName = Convert.ToString(row["firstName"]),
                            lastName = Convert.ToString(row["lastName"]),
                            contactPerson = Convert.ToString(row["contactPerson"]),
                            contactNo = Convert.ToString(row["contactNo"]),
                            email = Convert.ToString(row["email"]),
                            dateOfBirth = Convert.ToDateTime(row["dob"]),
                            age = Convert.ToInt32(row["age"]),
                            st_classRoomId = Convert.ToInt32(row["st_classRoomId"])
                        }
                    );
            }
            return student;

        }

        [HttpPost]
        [EnableCors]
        public IActionResult InsertStudent(Student stdobj)
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spStudentOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "insertStudent");
            command.Parameters.AddWithValue("@firstName", stdobj.firstName);
            command.Parameters.AddWithValue("@lastName", stdobj.lastName);
            command.Parameters.AddWithValue("@cntPerson", stdobj.contactPerson);
            command.Parameters.AddWithValue("@cntNumber", stdobj.contactNo);
            command.Parameters.AddWithValue("@email", stdobj.email);
            command.Parameters.AddWithValue("@dob", stdobj.dateOfBirth);
            command.Parameters.AddWithValue("@age", stdobj.age);
            command.Parameters.AddWithValue("@stClassRoomId", stdobj.st_classRoomId);
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
        public IActionResult UpdateStudent(Student studentObj) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spStudentOperations",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "updateStudent");
            command.Parameters.AddWithValue("@studentId", studentObj.studentId);
            command.Parameters.AddWithValue("@firstName", studentObj.firstName);
            command.Parameters.AddWithValue("@lastName", studentObj.lastName);
            command.Parameters.AddWithValue("@cntPerson", studentObj.contactPerson);
            command.Parameters.AddWithValue("@cntNumber", studentObj.contactNo);
            command.Parameters.AddWithValue("@email", studentObj.email);
            command.Parameters.AddWithValue("@dob", studentObj.dateOfBirth);
            command.Parameters.AddWithValue("@age", studentObj.age);
            command.Parameters.AddWithValue("@stClassRoomId", studentObj.st_classRoomId);
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

        //[HttpDelete]
        public IActionResult DeleteStudent(int studentId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spStudentOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deleteStudent");
            command.Parameters.AddWithValue("@studentId",studentId);
            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                return Ok();
            }
            else 
            {
                return BadRequest();
            }
        }

        public async Task<List<StudentDetails1>> GetStudentDetails1(int studentId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spGetStudentDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getStdDetails1");
            command.Parameters.AddWithValue("@studentId",studentId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var studentDetails = new List<StudentDetails1>();
            foreach (DataRow row in table.Rows) 
            {
                studentDetails.Add
                    (
                        new StudentDetails1 
                        {
                            stFirstName = Convert.ToString(row["firstName"]),
                            stLastName = Convert.ToString(row["lastName"]),
                            stContactNo = Convert.ToString(row["contactNo"]),
                            stContactPerson = Convert.ToString(row["contactPerson"]),
                            stEmail = Convert.ToString(row["email"]),
                            stDob = Convert.ToDateTime(row["dob"]),
                            stClassName = Convert.ToString(row["classRoomName"])
                        }
                    );
            }
            return studentDetails;
        }

        public async Task<List<StudentDetails2>> GetStudentDetails2(int studentId)
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spGetStudentDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getStdDetails2");
            command.Parameters.AddWithValue("@studentId", studentId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var studentDetails = new List<StudentDetails2>();
            foreach (DataRow row in table.Rows)
            {
                studentDetails.Add
                    (
                        new StudentDetails2
                        {
                            stSubjectName = Convert.ToString(row["subjectName"]),
                            stTeacherFirstName = Convert.ToString(row["teacherFirstName"]),
                            stTeacherLastName = Convert.ToString(row["teacherLastName"])
                        }
                    );
            }
            return studentDetails;
        }
    }
}
