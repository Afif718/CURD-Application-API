using FullStackWebAPI.Data;
using FullStackWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackWebAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        //this is the get method
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        //this is the post method
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

      

        // GET: api/Employees1/5
        [HttpGet("{id}")]
        //[Route("id:Guid")]
        public async Task<ActionResult<Employee>> GetEmployee( Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        //     [HttpPut]
        //     [Route("id:Guid")]
        //     public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        //     {
        //         var employee = await _fullStackDbContext.Employees.FindAsync(id);
        //
        //        if (employee == null)
        //        {
        //            return NotFound();
        //       }

        //        employee.Name = updateEmployeeRequest.Name;
        //       employee.Email = updateEmployeeRequest.Email;
        //       employee.Phone = updateEmployeeRequest.Phone;
        //       employee.Salary = updateEmployeeRequest.Salary;
        //       employee.Department = updateEmployeeRequest.Department;

        //      await _fullStackDbContext.SaveChangesAsync();

        //     return Ok(employee);
        // }
        //  }

        //  [HttpGet]
        //  [Route("id:Guid")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employees = await _fullStackDbContext.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            
            employees.Name = updateEmployeeRequest.Name;
            employees.Email = updateEmployeeRequest.Email;
            employees.Phone = updateEmployeeRequest.Phone;
            employees.Salary = updateEmployeeRequest.Salary;
            employees.Department = updateEmployeeRequest.Department;
            
            await _fullStackDbContext.SaveChangesAsync();
            
            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}
