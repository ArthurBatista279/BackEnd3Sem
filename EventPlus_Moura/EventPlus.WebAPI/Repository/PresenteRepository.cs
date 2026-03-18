using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repository;

public class PresenteRepository : IPresencaRepository
{
    private readonly EventoContext _context;
    public PresenteRepository(EventoContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Método que alterna a situacao da presença
    /// </summary>
    /// <param name="id">id da presenca a ser alterada</param>
    /// <param name="presenca"></param>
    public void Atualizar(Guid id, Presente presenca)
    {
        var presencaBuscada = _context.Presentes.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;

            _context.SaveChanges();
        }
        
    }
    public Presente BuscarPorId(Guid id)
    {
        return _context.Presentes
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id, Presente presenca)
    {
        var presencaBuscada = _context.Presentes.Find(id);
        if (presencaBuscada != null)
        {
            _context.Presentes.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }

    public void Inscrever(Presente presenca)
    {
        _context.Presentes.Add(presenca);
        _context.SaveChanges();
    }

    public List<Presente> Listar(Guid IdEvento)
    {
        return _context.Presentes
            .Include(p => p.IdUsuarioNavigation)
            .Where(p => p.IdEvento == IdEvento)
            .ToList();
    }
    /// <summary>
    /// Método que lista as presenças de um usuario especifico
    /// </summary>
    /// <param name="IdUsuario">id do usuario para filtragem</param>
    /// <returns>Lista de presencas de um usuario</returns>
    public List<Presente> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presentes
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
