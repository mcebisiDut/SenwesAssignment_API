using SenwesAssignment_Data.Models;
using System;
using System.Collections.Generic;

namespace SenwesAssignment_Data.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        IEnumerable<Employee> GetByJoiningDate(DateTime joiningDate);
        IEnumerable<Employee> GetByAge(int age);
        IEnumerable<Employee> GetTopPaidByGender(int numberOfEmployees, string gender);
        IEnumerable<Employee> GetByNamesAndCity(string name, string surname, string city);
        IEnumerable<int> GetSalariesByName(string name);
        IEnumerable<string> GetCities();
    }
}
