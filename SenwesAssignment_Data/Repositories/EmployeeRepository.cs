using Newtonsoft.Json;
using SenwesAssignment_Data.Interfaces;
using SenwesAssignment_Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SenwesAssignment_Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll()
        {
            return GetAllEmployees();
        }

        public Employee GetById(int id)
        {
            return GetAllEmployees().Where(employee => employee.EmpID == id).FirstOrDefault();
        }

        public IEnumerable<Employee> GetByJoiningDate(DateTime joiningDate)
        {
            return GetAllEmployees().Where(employee => DateTime.ParseExact(employee.DateOfJoining, "M/d/yyyy", null) >= joiningDate);
        }

        public IEnumerable<Employee> GetByAge(int age)
        {
            return GetAllEmployees().Where(employee => employee.Age > age);
        }

        public IEnumerable<Employee> GetTopPaidByGender(int numberOfEmployees, string gender)
        {
            return GetAllEmployees()
                .OrderByDescending(employee => employee.Salary)
                .Where
                (
                    employee => employee.Gender.ToLower() == gender.ToLower()
                 ).Take(numberOfEmployees);
        }

        public IEnumerable<Employee> GetByNamesAndCity(string name, string surname, string city)
        {
            return GetAllEmployees()
                .Where
                (
                    employee => employee.City.ToLower() == city.ToLower() &&
                                employee.FirstName.ToLower() == name.ToLower() ||
                                employee.LastName.ToLower() == surname.ToLower()
                );
        }

        public IEnumerable<int> GetSalariesByName(string name)
        {
            return GetAllEmployees()
                .Where(employee => employee.FirstName.ToLower() == name.ToLower())
                .Select(employee => employee.Salary);
        }

        public IEnumerable<string> GetCities()
        {
            return GetAllEmployees()
                .Select(employee => employee.City)
                .Where(cty => !string.IsNullOrWhiteSpace(cty))
                .Distinct();
        }

        private static IEnumerable<Employee> GetAllEmployees()
        {
            var jsonFilePath = GetFilePath();
            string json = File.ReadAllText(jsonFilePath);

            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);

            return employees;
        }

        private static string GetFilePath()
        {
            var parentDirectory = Path.GetFullPath("..\\SenwesAssignment_Data");
            var jsonFilePath = Path.Combine(parentDirectory, "Repositories\\Employee.json");

            return jsonFilePath;
        }
    }
}
