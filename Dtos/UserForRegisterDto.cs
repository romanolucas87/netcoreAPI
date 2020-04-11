using System.ComponentModel.DataAnnotations;

namespace netcoreAPI.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username {get; set;}

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 8 caracteres WACHIN")]
        public string Password {get; set;}
    }
}