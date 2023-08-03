using fullstackapi.Data;
using fullstackapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fullstackapi.Controllers
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

        [HttpGet]
     
        public async Task<IActionResult> GetAllEmployees()
        {

          var employee =await  _fullStackDbContext.Enployees.ToListAsync();
           
            return Ok(employee);

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee( [FromBody] Employee employeeRequest )
        {

            employeeRequest.Id = Guid.NewGuid();
           await _fullStackDbContext.Enployees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeRequest);

        }
         [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
         var employee= await   _fullStackDbContext.Enployees.FirstOrDefaultAsync(x=>x.Id==id);

            if (employee==null)
            {

                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]

         public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id , Employee updateEmployeeRequest)
        {

          var employee = await   _fullStackDbContext.Enployees.FindAsync(id);
             if (employee==null)
            {

                return NotFound();
            }
             employee.Name = updateEmployeeRequest.Name;
             employee.Email = updateEmployeeRequest.Email;
             employee.Phone = updateEmployeeRequest.Phone;
             employee.Salary = updateEmployeeRequest.Salary;
             employee.Department = updateEmployeeRequest.Department;

              await _fullStackDbContext.SaveChangesAsync();
             return Ok(employee);

        }

           [HttpDelete]
           [Route("{id:Guid}")]
           public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await   _fullStackDbContext.Enployees.FindAsync(id);
             if (employee==null)
            {

                return NotFound();
            }

             _fullStackDbContext.Enployees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);

        }


    }
}
