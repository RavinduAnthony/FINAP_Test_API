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
    public class SubjectAllocationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        Dbaccess dbaccess = new Dbaccess();
        public SubjectAllocationController(IConfiguration configuration, ILogger<SubjectController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SubjectAllocation(AllocatedSubject allocatedSubject) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectAllocationProcess",connection);
            command.CommandType = CommandType.StoredProcedure; 
            command.Parameters.AddWithValue("@action", "allocateSubject");
            command.Parameters.AddWithValue("@teacherId",allocatedSubject.teacherId);
            command.Parameters.AddWithValue("@subjectId",allocatedSubject.subjectID);
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
        public IActionResult SubjectDeAllocation(AllocatedSubject allocatedSubject) 
        {
            SqlConnection connection = dbaccess.getConnection(_configuration);
            SqlCommand command = new SqlCommand("spSubjectAllocationProcess", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@action", "deAllocateSubject");
            command.Parameters.AddWithValue("@teacherId", allocatedSubject.teacherId);
            command.Parameters.AddWithValue("@subjectId", allocatedSubject.subjectID);
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
