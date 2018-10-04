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
    [Route("api/image")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [Consumes("image/jpg")]
    public class ImageController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly ProjectCALServerContext _context;

        public ImageController(ProjectCALServerContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //maybe only for form on API?
        //[HttpGet]
        //public IActionResult Upload_Image()
        //{
        //    return View();
        //}

        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload_Image(IFormFile file)
        //{
        //    System.Diagnostics.Debugger.Break();
        //    //try
        //    //{
        //        var uploads = Path.Combine(_env.WebRootPath, "uploads");
        //        if (file.Length > 0)
        //        {
        //            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return ;
        //    //}
        //}

        /*
        // GET: api/Image
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Image/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Image
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Image/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
