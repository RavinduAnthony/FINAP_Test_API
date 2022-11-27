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
    public class ClassRoomController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();

        public ClassRoomController(IConfiguration configuration, ILogger<StudentController> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<ClassRoom>> GetAllClassRooms() 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassRoomOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getClassRooms");
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            var classRooms = new List<ClassRoom>();
            await Task.Run(() => { adapter.Fill(table); });
            foreach (DataRow row in table.Rows) 
            {
                classRooms.Add
                    (
                        new ClassRoom 
                        {
                            classRoomId = Convert.ToInt32(row["classRoomId"]),
                            classRoomName = Convert.ToString(row["classRoomName"])
                        }
                    );
            }
            return classRooms;
        }

        [HttpGet]
        public async Task<List<ClassRoom>> GetClassRoomById(int classRoomId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassRoomOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "getClassRoomById");
            command.Parameters.AddWithValue("@classRoomId", classRoomId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            await Task.Run(() => { adapter.Fill(table); });
            dbaccess.closeConnection();
            var classRooms = new List<ClassRoom>();
            foreach (DataRow row in table.Rows) 
            {
                classRooms.Add
                    (
                        new ClassRoom
                        {
                            classRoomId = Convert.ToInt32(row["classRoomId"]),
                            classRoomName = Convert.ToString(row["classRoomName"])
                        }
                    );
            }
            return classRooms;
        }

        [HttpPost]
        public IActionResult InsertClassRoom(ClassRoom classRoom) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassRoomOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "insertClassRoom");
            command.Parameters.AddWithValue("@classRoomName",classRoom.classRoomName);
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
        public IActionResult UpdateClassRoom(ClassRoom classRoom) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassRoomOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "updateClassRoom");
            command.Parameters.AddWithValue("@classRoomId", classRoom.classRoomId);
            command.Parameters.AddWithValue("@classRoomName",classRoom.classRoomName);
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

        
        public IActionResult DeleteClassRoom(int classRoomId) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassRoomOperations", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deleteClassRoom");
            command.Parameters.AddWithValue("@classRoomId", classRoomId);
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
