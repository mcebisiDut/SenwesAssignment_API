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
            try
            {
                var employeeData = _loadData.LoadEmployeeData();
                return Ok(employeeData);
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(new Exception(ex.ToString()), "Get all employees error", null);
                }

                return StatusCode(500, "Unexpected error occurred while trying to retrieve all employees");
            }
        }

        /// <summary>
        /// Get employee by employee id
        /// </summary>
        /// <returns>Returns a single employee</returns>
        [Route("Get/{empId}")]
        [HttpGet]
        public IActionResult GetByEmployeeId(int empId)
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().Where(x => x.EmpID == empId).FirstOrDefault();
                if (employee == null)
                {
                    return BadRequest("Employee not found");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(new Exception(ex.ToString()), $"Get employee by id: {empId} error ", null);
                }

                return StatusCode(500, "Unexpected error occurred while trying to retrieve employee");
            }
        }
    }
}
