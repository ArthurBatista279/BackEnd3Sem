using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid id);
    void Atualizar(Guid id, Evento evento);
    Evento buscar(Guid id);
    Evento buscarPorId(Guid id);
    List<Evento> ListarPorID(Guid IdUsuario);
    List<Evento> ListarProximos();
}