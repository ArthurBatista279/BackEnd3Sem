using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
    void Inscrever(Presente presenca);
    void Atualizar(Guid id, Presente presenca); 
    void Deletar(Guid id); 
    List<Presente> Listar(Guid IdEvento);
    Presente BuscarPorId(Guid id);
    List<Presente> ListarMinhas(Guid IdUsuario);
}