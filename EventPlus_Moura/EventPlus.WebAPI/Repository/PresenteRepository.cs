using EventPlus.WebAPI.BdContextEvento; 
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repository;

public class PresenteRepository : IPresencaRepository
{
    
    private readonly EventoContext _context;

    public PresenteRepository(EventoContext context)
    {
        _context = context;
    }

    public void Inscrever(Presente presenca)
    {
        _context.Presentes.Add(presenca);
        _context.SaveChanges(); 
    }

    public void Atualizar(Guid id, Presente presenca)
    {
        var presencaBuscada = _context.Presentes.Find(id);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = presenca.Situacao;

            _context.Presentes.Update(presencaBuscada);
            _context.SaveChanges();
        }
    }

    public void Deletar(Guid id)
    {
        var presencaBuscada = _context.Presentes.Find(id);

        if (presencaBuscada != null)
        {
            _context.Presentes.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }

    public List<Presente> Listar(Guid IdEvento)
    {
        return _context.Presentes.Where(p => p.IdEvento == IdEvento).ToList();
    }

    public List<Presente> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presentes.Where(p => p.IdUsuario == IdUsuario).ToList();
    }

    public Presente BuscarPorId(Guid id)
    {
        return _context.Presentes.Find(id)!;
    }
}