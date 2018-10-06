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

        //// POST: api/Utility/image
        //[HttpPost]
        //public void Post([FromBody] string image)
        //{

        //    byte[] test = Convert.FromBase64String(image);
        //}
    }
}
