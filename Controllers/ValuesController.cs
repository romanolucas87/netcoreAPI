using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    //http:localhost:5000/api/values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values// te retorna TODOS LOS VALORES
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //throw new Exception("Test Exception") EJECUTA UN TEST DE EXCEPCION para forzar el error;
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 // TRAE EL QUE COINCIDA CON EL IDENTIFICADOR NR0 5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return "Values";
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