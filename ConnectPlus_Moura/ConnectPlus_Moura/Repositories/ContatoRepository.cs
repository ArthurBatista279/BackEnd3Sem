using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus_Moura.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly EventoContext _context;

    public ContatoRepository(EventoContext context)
    {
        _context = context;
    }

    public async Task<List<Contato>> ListarAsync()
    {
        return await _context.Contatos
            .Include(c => c.IdTipoContatoNavigation)
            .ToListAsync();
    }

    public async Task<Contato?> BuscarPorIdAsync(Guid id)
    {
        return await _context.Contatos
            .Include(c => c.IdTipoContatoNavigation)
            .FirstOrDefaultAsync(c => c.IdContato == id);
    }

    public async Task<List<Contato>> ListarPorTipoContatoAsync(Guid idTipoContato)
    {
        return await _context.Contatos
            .Include(c => c.IdTipoContatoNavigation)
            .Where(c => c.IdTipoContato == idTipoContato)
            .ToListAsync();
    }

    public async Task<Contato> CadastrarAsync(Contato contato)
    {
        contato.IdContato = Guid.NewGuid();
        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();
        return contato;
    }

    public async Task AtualizarAsync(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Contato contato)
    {
        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
    }
}
