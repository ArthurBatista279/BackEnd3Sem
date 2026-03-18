using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{

    [Required(ErrorMessage = "O Nome do evento é obrigatório.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A Data do evento é obrigatório.")]
    public string? DataEvento { get; set; }

    [Required(ErrorMessage = "A Descrição do evento é obrigatório.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O tipo de evento do evento é obrigatório.")]
    public string? IdTipoEvento { get; set; }

    [Required(ErrorMessage = "A instituição do evento é obrigatório.")]
    public string? IdInstituicao { get; set; }


}
