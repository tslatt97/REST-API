using Employees.Models;
using System.Collections.Generic;

namespace Employees.Data
{
   public interface IEmployeesRepo
    {
        bool SaveChanges();
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int Id);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
