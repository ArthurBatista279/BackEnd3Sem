using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoUsuarioRepository
{
    void Cadastrar(TipoUsuario tipoUsuario);
    void Atualizar(Guid id, TipoUsuario tipoUsuario);
    List<TipoUsuario> Listar();
    TipoUsuario BuscarPorId(Guid id);
    void Deletar(Guid id);
}
