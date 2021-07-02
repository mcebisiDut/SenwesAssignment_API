using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SenwesAssignment_Library.Exceptions;
using SenwesAssignment_Library.Interfaces;
using System.Linq;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        static readonly string _badRequestMessage = "Requested resource not found";
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary> Get all employees </summary>
        ///<response code="200"> Returns all the available employees</response>
        ///<response code="404"> If no record matches were found</response>
        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAll();

            if (!employees.Any())
            {
                var title = "Get all";
                var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                return NotFound(notFoundObject);
            }

            return Ok(employees);
        }

        /// <summary> Get employee by their id </summary>
        ///<response code="200"> Returns employee resource that matches the given ID</response>
        ///<response code="400"> If given ID is invalid</response>
        ///<response code="404"> If no record matches the given filter</response>
        [Route("{empId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int empId)
        {
            var title = "Get by ID";
            try
            {
                var employee = _employeeService.GetById(empId);

                if (employee == null)
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                    return NotFound(notFoundObject);
                }

                return Ok(employee);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        /// <summary> All employees who joined in last {numberOfYears} years</summary>
        ///<response code="200"> Returns employees who joined the company in the space of years given</response>
        ///<response code="400"> If given number of years is invalid</response>
        ///<response code="404"> If no records matches the given filter</response>
        [Route("joinedInLast/{numberOfYears}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByNumberOfYearsJoined(int numberOfYears)
        {
            var title = "Get by number of years joined";
            try
            {
                var employeesJoinedInPastYears = _employeeService.GetByNumberOfYearsJoined(numberOfYears);

                if (!employeesJoinedInPastYears.Any())
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 400);

                    return NotFound(notFoundObject);
                }

                return Ok(employeesJoinedInPastYears);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        /// <summary> All employees older than {numberOfYears} years</summary>
        ///<response code="200"> Returns employees whose age is greater than given years</response>
        ///<response code="400"> If given number of years is invalid</response>
        ///<response code="404"> If no record matches the given filter</response>
        [Route("olderThan/{numberOfYears}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByAge(int numberOfYears)
        {
            var title = "Get by age";
            try
            {
                var employeesOldThanYears = _employeeService.GetByAge(numberOfYears);

                if (!employeesOldThanYears.Any())
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                    return NotFound(notFoundObject);
                }

                return Ok(employeesOldThanYears);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        /// <summary> Top {numberOfEmployees} of highest paid {gender} employees where Gender is: F or M </summary>
        ///<response code="200"> Returns certain number of highest paid employees whose gender matches the given gender</response>
        ///<response code="400"> If given number of years or gender is invalid</response>
        ///<response code="404"> If no record matches the given filter</response>
        [Route("top/{numberOfEmployees}/highestPaid/{gender}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult TopPaidEmployeesByGender(int numberOfEmployees, string gender)
        {
            var title = "Get highest paid";
            try
            {
                var topPaidEmployeesByGender = _employeeService.GetTopPaidByGender(numberOfEmployees, gender);

                if (!topPaidEmployeesByGender.Any())
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                    return NotFound(notFoundObject);
                }

                return Ok(topPaidEmployeesByGender);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        /// <summary> All employees with specific {name} or {surname} and {city} </summary>
        ///<response code="200"> Returns employees from given city whose name or surname matches the given name and surname</response>
        ///<response code="400"> If given name or surname or city is invalid</response>
        ///<response code="404"> If no record matches the given filter</response>
        [Route("by/{name}/{surname}/{city}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByNamesAndCity(string name, string surname, string city)
        {
            var title = "Get by city and names";
            try
            {
                var employeesByNamesAndCity = _employeeService.GetByNamesAndCity(name, surname, city);

                if (!employeesByNamesAndCity.Any())
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                    return NotFound();
                }

                return Ok(employeesByNamesAndCity);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        ///<summary> Employee salaries by their names </summary>
        ///<response code="200"> Returns salaries of all employees with given name</response>
        ///<response code="400"> If given name is invalid</response>
        ///<response code="404"> If no record matches the given filter</response>
        [Route("salaries/by/{name}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSalariesByName(string name)
        {
            var title = "Get salaries";
            try
            {
                var employeesSalariesByName = _employeeService.GetSalariesByName(name);

                if (!employeesSalariesByName.Any())
                {
                    var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                    return NotFound(notFoundObject);
                }

                return Ok(employeesSalariesByName);
            }
            catch (InvalidUserInputException exception)
            {
                var badRequestObject = GetErrorObject(title, exception.Message, 400);
                _logger.LogError(exception.Message);

                return BadRequest(badRequestObject);
            }
        }

        ///<summary> All cities </summary>
        ///<response code="200"> Returns all cities with no duplicates</response>
        ///<response code="404"> If no records were found</response>
        [Route("cities")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCities()
        {
            var title = "Get cities";
            var allCities = _employeeService.GetCities();

            if (!allCities.Any())
            {
                var notFoundObject = GetErrorObject(title, _badRequestMessage, 404);

                return NotFound(notFoundObject);
            }

            return Ok(allCities);
        }

        private static object GetErrorObject(string title, string detail, int code)
        {
            return new
            {
                type = "Get",
                title,
                status = code,
                detail,
                instance = "Employee"
            };
        }
    }
}
