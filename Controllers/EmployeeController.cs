using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SenwesAssignment_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly LoadData _loadData;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _loadData = new LoadData();
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var employeeData = _loadData.LoadEmployeeData();
            return Ok(employeeData);
        }

        
        [Route("Get/{empId}")]
        [HttpGet]
        public IActionResult GetByEmployeeId(int empId)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.EmpID == empId).FirstOrDefault();
            return Ok(employee);
        }
    }
}
