using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar (ComentarioEvento comentarioEvento);
    void Deletar (Guid id);
    List<ComentarioEvento> List(Guid IdEvento);
    ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento);
}
