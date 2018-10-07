using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORE_WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CORE_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    [EnableCors("MyPolicy")]
    public class EmployeesController : Controller
    {
        private readonly ProjectCALServerContext _context;
        private readonly IHostingEnvironment _env;
        private string baseURL = "C:\\img\\employees\\";

        public EmployeesController(ProjectCALServerContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            return _context.Employee.Include(accessRole => accessRole.AccessRole);
        }

        // GET: /api/employees/image/13
        [HttpGet("image/{id}")]
        public IActionResult GetEmployeeImage(int id)
        {
            List<string> files = new List<string>();
            files.Add(".bmp");
            files.Add(".jpeg");
            files.Add(".jpg");
            files.Add(".png");
            files.Add(".tif");
            files.Add(".tiff");

            try
            {
                foreach (string filetype in files)
                {
                    if (System.IO.File.Exists(baseURL + id + filetype))
                    {
                        byte[] imageByte = System.IO.File.ReadAllBytes(baseURL + id + filetype);
                        return File(imageByte, "image/*");
                    }
                }
                return NotFound("File was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /employeegrid
        [HttpGet("/employeegrid")]
        public EmployeeGrid EmployeeGrid()
        {
            EmployeeGrid grid = new EmployeeGrid();

            grid.totalCount = _context.Employee.Include(accessRole => accessRole.AccessRole).Count();

            grid.employees = _context.Employee.Include(accessRole => accessRole.AccessRole);
            return grid;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee.Include(accessRole => accessRole.AccessRole)
                                                 .SingleOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET: api/Employees/phone/012....
        [HttpGet("phone/{id}")]
        public async Task<IActionResult> GetEmployeeByPhone([FromRoute] string id)
        {
            System.Diagnostics.Debugger.Break();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employee
                                                   .SingleOrDefaultAsync(m => m.EmployeePhone == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        //Verify Employee Password
        [HttpPost("verify/{id}")]
        public async Task<IActionResult> VerifyPassword([FromRoute] string id, [FromBody] Employee employee)
        {
            System.Diagnostics.Debugger.Break();
            bool valid = false;
            try
            {

                var log = await _context.Employee
                                            .SingleOrDefaultAsync(m => m.EmployeePhone == id);

                if (log == null)
                {
                    return NotFound("No Login linked to this cell number.");
                }
                if (log.verifyPassword(employee.EmployeePassword))
                {
                    valid = true;
                    return Ok(valid);
                }
                else return BadRequest(valid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            System.Diagnostics.Debugger.Break();
            Employee updateEmployee = _context.Employee.FirstOrDefault(e => e.EmployeeId == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateEmployee.EmployeeId)
            {
                return BadRequest();
            }

            updateEmployee.UpdateChangedFields(employee);

            updateEmployee.hashPassword();


            _context.Entry(updateEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            employee.hashPassword();
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        //POST: /api/Employees/image/{id}
        [HttpPost("image/{id}")]
        public async Task<IActionResult> UploadPackageTypeImage(IFormFile file, int id)
        {
            System.Diagnostics.Debugger.Break();
            try
            {
                if (file != null)
                {
                    string ext = System.IO.Path.GetExtension(file.FileName);
                    var fileName = Path.Combine(baseURL, id.ToString() + ext);

                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }

                return Ok("Upload successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Employee employee = await _context.Employee.Include(m => m.Application).SingleOrDefaultAsync(m => m.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound("The Employee was not found.");
                }

                if (employee.Application.Count > 0)// || ||)
                {
                    return BadRequest("The selected Employee cannot be deleted because they have existing reviewed applications.");
                }
                else
                {
                    _context.Employee.Remove(employee);
                    await _context.SaveChangesAsync();

                    return Ok(employee);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}