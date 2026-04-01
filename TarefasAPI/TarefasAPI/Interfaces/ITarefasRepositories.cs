using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using TarefasAPI.Models;

namespace TarefasAPI.Interfaces;

public interface ITarefasRepositories
{
    void Cadastrar(Tarefa tarefas);
    List<Tarefa> Listar();
    void Deletar (Guid id);
    void Atualizar (Guid id, Tarefa tarefas);
    Tarefa BuscarPorId (Guid id);

}
