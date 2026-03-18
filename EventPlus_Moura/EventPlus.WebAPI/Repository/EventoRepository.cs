using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repository;

public class EventoRepository : IEventoRepository
{
    private readonly EventoContext _context;
    public EventoRepository(EventoContext context)
    { 
        _context = context; 
    }
    public void Atualizar(Guid id, Evento evento)
    {
        var Evento = _context.Eventos.Find(id);

        if (Evento != null)
        {
            Evento.Nome = evento.Nome;

            _context.SaveChanges();
        }
    }

    public Evento buscar(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }

    public Evento buscarPorId(Guid id)
    {
        return _context.Eventos.FirstOrDefault(e => e.IdEvento == id)!;
    }

    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var Evento = _context.Eventos.Find(id);

        if (Evento != null)
        {
            _context.Eventos.Remove(Evento);
            _context.SaveChanges();
        }
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.ToList();
    }
    /// <summary>
    /// Método que lista eventos filtrando pelas presenças de um USER
    /// </summary>
    /// <param name="IdUsuario">Id do Usuario para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuario</returns>
    public List<Evento> ListarPorID(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presentes.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }
    /// <summary>
    /// Método que busca os proximos eventos que irão acontecer
    /// </summary>
    /// <returns>Lista de proximos eventos</returns>
    public List<Evento> ListarProximos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
