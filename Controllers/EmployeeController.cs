using EmployeeManAPI.Data;
using EmployeeManAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManAPI.Controllers
{
    //[Route("api/[controller]")]
    //    [ApiController]

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly AppDbContext _Context;

        public EmployeeController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _Context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeByID(int id)
        {
            var _employee = await _Context.Employees.FindAsync(id);
            if(_employee == null)
            {
                return NotFound();
            }

            return _employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _Context.Employees.Add(employee);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeByID), new { id = employee.Id }, employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if(id != employee.Id)
            {
                return BadRequest();
            }

            _Context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
             
                if(!EmployeeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _Context.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            _Context.Employees.Remove(employee);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _Context.Employees.Any(a => a.Id == id);
        }


    }
}
