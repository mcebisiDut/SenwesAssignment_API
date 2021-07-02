using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SenwesAssignment_Data.Interfaces;
using SenwesAssignment_Data.Models;
using SenwesAssignment_Library.Exceptions;
using SenwesAssignment_Library.Services;
using System.Collections.Generic;
using System.Linq;

namespace SenwesAssignment_Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        [Test]
        public void GetByAll_Given_There_Are_Existing_Records_Should_Return_All_Employee_Records()
        {
            //------------------------------Arrange-------------------------------
            var expectedEmployees = GetEmployees();
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var employeeService = CreateEmployeeService(employeeRepository);

            //------------------------------Act-----------------------------------
            employeeRepository.GetAll().Returns(expectedEmployees);
            var actual = employeeService.GetAll();

            //------------------------------Assert--------------------------------
            actual.Should().BeEquivalentTo(expectedEmployees);
            actual.Count().Should().Be(3);
            employeeRepository.Received(1).GetAll();

        }

        [TestCase(0)]
        [TestCase(-10)]
        [TestCase(-100)]
        public void GetById_Given_Invalid_Employee_Id_Should_Throw_Exception(int employeeId)
        {
            //------------------------------Arrange-------------------------------
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var employeeService = CreateEmployeeService(employeeRepository);
            var expectedErrorMessage = "Invalid employee ID, make sure that ID is greater than Zero (0)";

            //------------------------------Act-----------------------------------
            var exception = Assert.Throws<InvalidUserInputException>(() => employeeService.GetById(employeeId));

            //------------------------------Assert--------------------------------
            exception.Message.Should().Be(expectedErrorMessage);

        }

        [Test]
        public void GetById_Given_Valid_Employe_Id_Should_Return_Employee_Record()
        {
            //------------------------------Arrange-------------------------------
            var employeeId = 850000;
            var expectedEmployee = GetEmployee();
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var employeeService = CreateEmployeeService(employeeRepository);

            //------------------------------Act-----------------------------------
            employeeRepository.GetById(employeeId).Returns(expectedEmployee);
            var actual = employeeService.GetById(employeeId);

            //------------------------------Assert--------------------------------
            actual.Should().BeEquivalentTo(expectedEmployee);
            employeeRepository.Received(1).GetById(employeeId);

        }

        [Test]
        public void GetAllCities_Given_There_Are_Existing_Records_Should_Return_All_Cities()
        {
            //------------------------------Arrange-------------------------------
            var expectedCities = new List<string> { "Las Vegas", "Irons", "Lexington" };
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var employeeService = CreateEmployeeService(employeeRepository);

            //------------------------------Act-----------------------------------
            employeeRepository.GetCities().Returns(expectedCities);
            var actual = employeeService.GetCities();

            //------------------------------Assert--------------------------------
            actual.Should().BeEquivalentTo(expectedCities);
            employeeRepository.Received(1).GetCities();
            actual.Count().Should().Be(3);

        }

        [Test]
        public void GetSalariesByName_Given_Employee_Name_Exists_Should_Return_All_Salaries_Of_Employees_With_That_Name()
        {
            //------------------------------Arrange-------------------------------
            var firstName = "Treasure";
            var expectedSalaries = new List<int> { 119093, 213400, 45900 };
            var employeeRepository = Substitute.For<IEmployeeRepository>();
            var employeeService = CreateEmployeeService(employeeRepository);

            //------------------------------Act-----------------------------------
            employeeRepository.GetSalariesByName(firstName).Returns(expectedSalaries);
            var actual = employeeService.GetSalariesByName(firstName);

            //------------------------------Assert--------------------------------
            actual.Should().BeEquivalentTo(expectedSalaries);
            employeeRepository.Received(1).GetSalariesByName(firstName);
            actual.Count().Should().Be(3);

        }

        private static Employee GetEmployee()
        {
            var employee = new Employee
            {
                EmpID = 850297,
                FirstName = "Shawna",
                LastName = "Buck",
                Gender = "F",
                EMail = "shawna.buck@gmail.com",
                DateOfBirth = "12/12/1971",
                Age = 45.66,
                DateOfJoining = "12 / 18 / 2010",
                YearsInCompany = 6.61,
                Salary = 119090,
                LastIncrease = "17%",
                SSN = "222 - 11 - 7603",
                PhoneNo = "702 - 771 - 7149",
                City = "Las Vegas",
                State = "NV",
                Zip = 89128,
                UserName = "swbuck"
            };

            return employee;
        }

        private static IEnumerable<Employee> GetEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    EmpID = 8503233,
                    FirstName = "First",
                    LastName = "Test",
                    Gender = "F",
                    EMail = "first.test@gmail.com",
                    DateOfBirth = "12/12/1971",
                    Age = 45.66,
                    DateOfJoining = "12/18/2010",
                    YearsInCompany = 6.61,
                    Salary = 119090,
                    LastIncrease = "17%",
                    SSN = "222-11-7603",
                    PhoneNo = "702-771-7149",
                    City = "Las Vegas",
                    State = "NV",
                    Zip = 89128,
                    UserName = "swbuck"
                },
                new Employee
                {
                    EmpID = 850007,
                    FirstName = "Secod",
                    LastName = "Test",
                    Gender = "F",
                    EMail = "second.test@gmail.com",
                    DateOfBirth = "12/12/1971",
                    Age = 45.66,
                    DateOfJoining = "12/18/2010",
                    YearsInCompany = 6.61,
                    Salary = 119090,
                    LastIncrease = "17%",
                    SSN = "222-11-7603",
                    PhoneNo = "702-771-7149",
                    City = "Las Vegas",
                    State = "NV",
                    Zip = 89128,
                    UserName = "swbuck"
                },
                new Employee
                {
                    EmpID = 850001,
                    FirstName = "Third",
                    LastName = "Test",
                    Gender = "F",
                    EMail = "third.test@gmail.com",
                    DateOfBirth = "12/12/1971",
                    Age = 45.66,
                    DateOfJoining = "12/18/2010",
                    YearsInCompany = 6.61,
                    Salary = 119090,
                    LastIncrease = "17%",
                    SSN = "222-11-7603",
                    PhoneNo = "702-771-7149",
                    City = "Las Vegas",
                    State = "NV",
                    Zip = 89128,
                    UserName = "swbuck"
                }
            };

            return employees;
        }

        private static EmployeeService CreateEmployeeService(IEmployeeRepository employeeRepository)
        {
            return new EmployeeService(employeeRepository);
        }
    }
}
