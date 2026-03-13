using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O nome do Usuário é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O e-mail do Usuário é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O senha do Usuário é obrigatório!")]
    public string? Senha { get; set; }
    public Guid IdTipoUsuario { get; set; }

}
