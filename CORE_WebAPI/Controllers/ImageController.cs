using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CORE_WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CORE_WebAPI.Controllers
{
    [Produces("application/json")] //not sure about this
    [Route("api/image")]
    [EnableCors("MyPolicy")]
    //[Consumes("image/jpg")] //not sure about this
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _env; //hosting environment to find a root path
        private readonly ProjectCALServerContext _context;
        
        public ImageController(ProjectCALServerContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //POST: /api/image/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload_Image(IFormFile file)
        {
            System.Diagnostics.Debugger.Break();
            try
            {
                if (file != null)
                {
                    var fileName = Path.Combine(_env.WebRootPath, Path.GetFileName(file.FileName)); //set new filename & get extention

                    //var fileName = Path.Combine(packageURL, packageID + Path.GetExtention(file.FileName)); 

                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        //set filename to ID of the thing uploaded
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
    }
}
