using TarefasAPI.BdContextTarefas;
using TarefasAPI.Interfaces;
using TarefasAPI.Models;

namespace TarefasAPI.Repositories;

public class TarefasRepositories : ITarefasRepositories
{
    private readonly TarefasContext _tarefasContext;

    public TarefasRepositories(TarefasContext tarefasContext)
    {
        _tarefasContext = tarefasContext;
    }

    public void Atualizar(Guid id, Tarefa tarefas)
    {
        var tarefa = _tarefasContext.Tarefas.Find(id);

        if (tarefa != null)
        {
            tarefa.Titulo = tarefas.Titulo;
            tarefa.Descricao = tarefas.Descricao;
            tarefa.StatusDeConclusao = tarefas.StatusDeConclusao;
           
            _tarefasContext.SaveChanges();
        }
    }

    public Tarefa BuscarPorId(Guid id)
    {
        return _tarefasContext.Tarefas.Find(id)!;
    }

    public void Cadastrar(Tarefa tarefas)
    {
        _tarefasContext.Tarefas.Add(tarefas);
        _tarefasContext.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var tarefas = _tarefasContext.Tarefas.Find(id);

        if (tarefas != null)
        {
            _tarefasContext.Tarefas.Remove(tarefas);
            _tarefasContext.SaveChanges();
        }
    }

    public List<Tarefa> Listar()
    {
        return _tarefasContext.Tarefas.ToList();
    }
}
