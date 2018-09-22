using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CORE_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    [EnableCors("MyPolicy")]
    public class LogoController : ControllerBase
    {

        private string baseURL = "C:\\img\\";

        // GET: /api/Logo
        [HttpGet]
        public IActionResult GetLogo()
        {
            byte[] imageByte = System.IO.File.ReadAllBytes(baseURL + "projectcal" + ".png");
            return File(imageByte, "image/png");
        }

        //// GET: api/Logo
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        //// GET: api/Logo/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Logo
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Logo/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
