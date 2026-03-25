using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;

namespace ConnectPlus_Moura.Interfaces;

public interface ITipoContatoRepository
{
    Task<List<TipoContato>> ListarAsync();
    Task<TipoContato?> BuscarPorIdAsync(Guid id);
    Task<TipoContato> CadastrarAsync(TipoContato tipoContato);
    Task AtualizarAsync(TipoContato tipoContato);
    Task DeletarAsync(TipoContato tipoContato);
}
