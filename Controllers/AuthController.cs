using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcoreAPI.Data;
using netcoreAPI.Dtos;
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

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //Validacion del modelo.. AGREGAR [FromBody]
           // if (!ModelState.IsValid)
           //     return BadRequest(ModelState);

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            
            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("El Usuario ya existe");
            
            var userToCreate = new User {
            Username =userForRegisterDto.Username
            };

            var createdUser = await _repo.Register (userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

    }
}