using SenwesAssignment_Data.Models;
using System.Collections.Generic;

namespace SenwesAssignment_Library.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        IEnumerable<Employee> GetByNumberOfYearsJoined(int numberOfYears);
        IEnumerable<Employee> GetByAge(int age);
        IEnumerable<Employee> GetTopPaidByGender(int numberOfEmployees, string gender);
        IEnumerable<Employee> GetByNamesAndCity(string name, string surname, string city);
        IEnumerable<int> GetSalariesByName(string name);
        IEnumerable<string> GetCities();
    }
}
