using SenwesAssignment_Data.Interfaces;
using SenwesAssignment_Data.Models;
using SenwesAssignment_Library.Exceptions;
using SenwesAssignment_Library.Interfaces;
using System;
using System.Collections.Generic;

namespace SenwesAssignment_Library.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetById(int id)
        {
            if (id < 1)
                throw new InvalidUserInputException("Invalid employee ID, make sure that ID is greater than Zero (0)");

            return _employeeRepository.GetById(id);
        }

        public IEnumerable<Employee> GetByNumberOfYearsJoined(int numberOfYears)
        {
            if (numberOfYears < 1)
                throw new InvalidUserInputException("Invalid number of years, make sure that number of years is greater than Zero (0)");

            var joiningYear = DateTime.Now.AddYears(-numberOfYears).Year;
            var joiningDate = new DateTime(joiningYear, 1, 1);

            return _employeeRepository.GetByJoiningDate(joiningDate);
        }

        public IEnumerable<Employee> GetByAge(int age)
        {
            if (age < 1)
                throw new InvalidUserInputException("Given age is invalid, make sure that age is greater than Zero (0)");

            return _employeeRepository.GetByAge(age);
        }

        public IEnumerable<Employee> GetTopPaidByGender(int numberOfEmployees, string gender)
        {
            if (numberOfEmployees < 1 || Invalid(gender))
                throw new InvalidUserInputException("Given inputs are invalid, make sure that age is greater than Zero (0) and gender is: female or male");

            return _employeeRepository.GetTopPaidByGender(numberOfEmployees, gender);
        }

        public IEnumerable<Employee> GetByNamesAndCity(string name, string surname, string city)
        {
            if (InvalidString(name) || InvalidString(surname) || InvalidString(city))
                throw new InvalidUserInputException("Given inputs are invalid, make sure that Name, Surname and City are valid.");

            return _employeeRepository.GetByNamesAndCity(name, surname, city);
        }

        public IEnumerable<int> GetSalariesByName(string name)
        {
            if (InvalidString(name))
                throw new InvalidUserInputException("Given inputs are invalid, make sure that Name is valid.");

            return _employeeRepository.GetSalariesByName(name);
        }

        public IEnumerable<string> GetCities()
        {
            return _employeeRepository.GetCities();
        }

        private static bool Invalid(string gender)
        {
            return string.IsNullOrWhiteSpace(gender) || !(Valid(gender));
        }

        private static bool InvalidString(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        private static bool Valid(string gender)
        {
            return (gender.ToLower() == "f" || gender.ToLower() == "m");
        }
    }
}
