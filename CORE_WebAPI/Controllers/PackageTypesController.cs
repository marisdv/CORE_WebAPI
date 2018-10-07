using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CORE_WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CORE_WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/PackageTypes")]
    [EnableCors("MyPolicy")]
    public class PackageTypesController : Controller
    {
        private readonly ProjectCALServerContext _context;
        private readonly IHostingEnvironment _env;
        private string baseURL = "C:\\img\\packages\\";

        public PackageTypesController(ProjectCALServerContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/PackageTypes
        [HttpGet]
        public IEnumerable<PackageType> GetPackageType()
        {
            List<PackageType> packageType = _context.PackageType.ToList();
            packageType.ForEach(x=> x.PackageTypeImage = null);
            return packageType;
        }
        
        // GET: /api/package/image/4
        [HttpGet("image/{id}")]
        public IActionResult GetPackageImage(int id)
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
                    if(System.IO.File.Exists(baseURL + id + filetype))
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
                throw;
            }
        }

        // GET: /packagetypesgrid
        [HttpGet("/packagetypesgrid")]
        public PackageTypeGrid PackageTypeGrid()
        {
            PackageTypeGrid grid = new PackageTypeGrid();

            grid.totalCount = _context.PackageType.Count();

            grid.packageTypes = _context.PackageType;

            return grid;
        }

        // GET: api/PackageTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageType = await _context.PackageType.SingleOrDefaultAsync(m => m.PackageTypeId == id);
            packageType.PackageTypeImage = null;
            if (packageType == null)
            {
                return NotFound();
            }

            return Ok(packageType);
        }

        // PUT: api/PackageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageType([FromRoute] int id, [FromBody] PackageType packageType)
        {
            System.Diagnostics.Debugger.Break();

            PackageType updatePackageType = _context.PackageType.FirstOrDefault(p => p.PackageTypeId == id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatePackageType.PackageTypeId)
            {
                return BadRequest();
            }


            updatePackageType.UpdateChangedFields(packageType);

            _context.Entry(updatePackageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageTypeExists(id))
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

        // POST: api/PackageTypes
        [HttpPost]
        public async Task<IActionResult> PostPackageType([FromBody] PackageType packageType)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.PackageType.Add(packageType);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPackageType", new { id = packageType.PackageTypeId }, packageType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            
        }

        //POST: /api/PackageTypes/image/{id}
        [HttpPost("image/{id}")]
        public async Task<IActionResult> UploadPackageTypeImage(IFormFile file, int id)
        {
            System.Diagnostics.Debugger.Break();
            try
            {
                if (file != null)
                {
                    //var fileName = Path.Combine(baseURL, Path.GetFileName(file.FileName)); //set new filename & get extention

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

        // DELETE: api/PackageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageType([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //should package be included?
                PackageType packageType = await _context.PackageType.Include(v => v.VehiclePacakageLine).Include(p => p.Package).SingleOrDefaultAsync(v => v.PackageTypeId == id);

                System.Diagnostics.Debugger.Break();
                if (packageType == null)
                {
                    return NotFound("The Package Type was not found.");
                }

                System.Diagnostics.Debugger.Break();

                if (packageType.Package.Count > 0)
                {
                    return BadRequest("The selected Package Type cannot be deleted because it is assigned to a Package.");
                }
                else
                {
                    _context.PackageType.Remove(packageType);

                    //also delete vehicle package lines where this package type is used

                    await _context.SaveChangesAsync();

                    return Ok(packageType);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return BadRequest(ex.Message);
            }
        }

        private bool PackageTypeExists(int id)
        {
            return _context.PackageType.Any(e => e.PackageTypeId == id);
        }
    }
}