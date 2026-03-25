using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repository;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventoContext _context;

    public ComentarioEventoRepository(EventoContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um comentário de evento
    /// </summary>
    /// <param name="id">Id do comentário a ser atualizado</param>
    /// <param name="comentarioEvento">Novos dados do comentário</param>
    public void Atualizar(Guid id, ComentarioEvento comentarioEvento)
    {
        var comentarioBuscado = _context.ComentarioEventos.Find(id);

        if (comentarioBuscado != null)
        {
            comentarioBuscado.Descricao = comentarioEvento.Descricao;
            comentarioBuscado.Exibe = comentarioEvento.Exibe;
            comentarioBuscado.DataComentarioEvento = comentarioEvento.DataComentarioEvento;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um comentário por id
    /// </summary>
    /// <param name="id">Id do comentário a ser buscado</param>
    /// <returns>Objeto ComentarioEvento com as informações do comentário buscado</returns>
    public ComentarioEvento BuscarPorId(Guid id)
    {
        return _context.ComentarioEventos.Find(id)!;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Cadastra um novo comentário de evento
    /// </summary>
    /// <param name="comentarioEvento">Comentário a ser cadastrado</param>
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um comentário de evento
    /// </summary>
    /// <param name="id">Id do comentário a ser deletado</param>
    public void Deletar(Guid id)
    {
        var comentarioBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Lista todos os comentários de um evento
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Lista de ComentarioEvento</returns>
    public List<ComentarioEvento> List(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Where(c => c.IdEvento == IdEvento)
            .OrderBy(c => c.DataComentarioEvento)
            .ToList();
    }
    /// <summary>
    /// Lista somente os comentários visíveis (Exibe = true) de um evento
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Lista de ComentarioEvento com Exibe = true</returns>
    public List<ComentarioEvento> ListarSomenteExibe(Guid idEvento)
    {
        return _context.ComentarioEventos
            .Where(c => c.IdEvento == idEvento && c.Exibe)
            .OrderBy(c => c.DataComentarioEvento)
            .ToList();
    }
}

