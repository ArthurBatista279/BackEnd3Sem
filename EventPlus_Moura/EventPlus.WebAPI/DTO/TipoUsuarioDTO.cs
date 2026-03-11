using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class TipoUsuarioDTO
{
    [Required(ErrorMessage = "O Titulo do tipo de usuario e obrigatorio")]
    public string? Titulo { get; set; }
}
