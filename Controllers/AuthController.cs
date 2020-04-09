using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcoreAPI.Data;
using netcoreAPI.Models;

namespace netcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo){
            _repo = repo;
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register (string username, string password)//vamos a tener que cambiar o convertir los parametros
        {
            //validate request
            username = username.ToLower();
            if(await _repo.UserExists(username))
                return BadRequest("El Usuario ya existe");
            var userToCreate = new User {
            Username = username
            };

            var createdUser = await _repo.Register (userToCreate, password);
            return StatusCode(201);
        }

    }
}