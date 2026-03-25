namespace ConnectPlus_Moura.DTO;

public class ContatoDTO
{
    public string Nome { get; set; } = string.Empty;
    public string DadosDeContato { get; set; } = string.Empty;
    public string? Imagem { get; set; }
    public Guid IdTipoContato { get; set; }
}
