using Assignment_1.Controllers;
using Assignment_1.Models;
namespace Assignment_1.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly Context context;
        public EmployeeRepository(Context _context)
        {
            context= _context;
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = context.employees.ToList();
            return employees;
        }

        public Employee GetById(int id)
        {
            return context.employees.FirstOrDefault(x=>x.Id==id);
        }
        public void Insert(Employee employee)
        {
            context.employees.Add(employee);
            context.SaveChanges();
        }

        public void Update(int id, Employee employee)
        {
            Employee oldEmp= GetById(id);
            oldEmp.Name = employee.Name;
            oldEmp.Address = employee.Address;
            oldEmp.Salary= employee.Salary;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Employee employee = GetById(id);
            context.employees.Remove(employee);
            context.SaveChanges();
        }

    }
}
