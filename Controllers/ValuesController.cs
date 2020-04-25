using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcoreAPI.Data;

namespace DatingApp.API.Controllers
{
    //http:localhost:5000/api/values
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            this._context = context;            
        }
    // GET api/values// te retorna TODOS LOS VALORES
    [HttpGet]
    public async Task<IActionResult> GetValues()
    {
        //throw new Exception("Test Exception") EJECUTA UN TEST DE EXCEPCION para forzar el error;
       var values = await _context.Values.ToListAsync();
       return Ok(values);
    }
    
    [AllowAnonymous]
    // GET api/values/5 // TRAE EL QUE COINCIDA CON EL IDENTIFICADOR NR0 5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(int id)
    {
        var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

        return Ok(value);
      //  return BadRequest("API NOT FOUND");
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)//FROMBODY es el cuerpo en la request que quiero crear o modificar//
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
}