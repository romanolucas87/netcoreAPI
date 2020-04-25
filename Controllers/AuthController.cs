using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcoreAPI.Data;
using netcoreAPI.Dtos;
using netcoreAPI.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace netcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config){
            _repo = repo;
            _config = config;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){

            var userFromRepo= await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)//valida el usuario
                return Unauthorized();
            
            var claims = new[]//crea claim que reune la informacion que el token necesita
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);//crea las credenciales

            var tokenDescriptor = new SecurityTokenDescriptor //es lo que necesita el JWT con la informacion
             {
                Subject = new ClaimsIdentity(claims),// datos del usuario (ID Y USERNAME)
                Expires = DateTime.Now.AddDays(1),//cuanto tiempo el usuario puede utilizar el token
                SigningCredentials = creds // las credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler(); //PAtrones de diseño - "CREACIONALES"

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new {
                token = tokenHandler.WriteToken(token)//crea el json de respuesta con la informacion que tenga el token
            });

        }

    }
}