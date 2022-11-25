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
    public class ClassRoomAllocationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();

        public ClassRoomAllocationController(IConfiguration configuration, ILogger<ClassRoomAllocationController> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AllocateClassRoom(AllocatedClass allocatedClass) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassAllocationProcess", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "allocateClassRoom");
            command.Parameters.AddWithValue("@teacherId",allocatedClass.teacherId);
            command.Parameters.AddWithValue("@classId",allocatedClass.classId);
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

        [HttpPost]
        public IActionResult DeAllocateClassRoom(AllocatedClass allocatedClass) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spClassAllocationProcess", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deAllocateClass");
            command.Parameters.AddWithValue("@teacherId", allocatedClass.teacherId);
            command.Parameters.AddWithValue("@classId", allocatedClass.classId);
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
