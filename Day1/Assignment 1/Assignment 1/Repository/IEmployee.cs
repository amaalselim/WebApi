using Assignment_1.Controllers;
using Assignment_1.Models;

namespace Assignment_1.Repository
{
    public interface IEmployee
    {
        List<Employee> GetAll();
        Employee GetById(int id);
        void Insert(Employee employee);
        void Update(int id,Employee employee);
        void Delete(int id);
    }
}
