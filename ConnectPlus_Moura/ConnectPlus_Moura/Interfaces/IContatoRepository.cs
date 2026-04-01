using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;

namespace ConnectPlus_Moura.Interfaces;

public interface IContatoRepository
{
    Task<List<Contato>> ListarAsync(); 
    Task<Contato?> BuscarPorIdAsync(Guid id);
    Task<List<Contato>> ListarPorTipoContatoAsync(Guid idTipoContato);
    Task<Contato> CadastrarAsync(Contato contato);
    Task AtualizarAsync(Contato contato);
    Task DeletarAsync(Contato contato);
}
