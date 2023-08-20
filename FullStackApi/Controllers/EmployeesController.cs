using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext _fullStackDBContext;

        public EmployeesController(FullStackDBContext fullStackDBContext)
        {
           _fullStackDBContext = fullStackDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {

            var employees =  await _fullStackDBContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee emploueeRequest)
        {
            emploueeRequest.Id = Guid.NewGuid();
            await _fullStackDBContext.Employees.AddAsync(emploueeRequest);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(emploueeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var emp = await _fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        { 
            var emp = await _fullStackDBContext.Employees.FindAsync(id);
            if (emp == null) {
                return NotFound();
            }

            emp.Name = updateEmployeeRequest.Name;
            emp.Salary = updateEmployeeRequest.Salary;
            emp.Email = updateEmployeeRequest.Email;
            emp.Phone = updateEmployeeRequest.Phone;
            emp.Department = updateEmployeeRequest.Department;
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var emp = await _fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
             _fullStackDBContext.Employees.Remove(emp);
            await _fullStackDBContext.SaveChangesAsync();
            return Ok(emp);
        }

    }
}
