using ConnectPlus_Moura.BdContextEvento;
using Microsoft.EntityFrameworkCore;
using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;
using Microsoft.EntityFrameworkCore;

namespace ConnectPlus_Moura.Repositories;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly EventoContext _context;

    public TipoContatoRepository(EventoContext context)
    {
        _context = context;
    }

    public async Task<List<TipoContato>> ListarAsync()
    {
        return await _context.TipoContatos.ToListAsync();
    }

    public async Task<TipoContato?> BuscarPorIdAsync(Guid id)
    {
        return await _context.TipoContatos.FindAsync(id);
    }

    public async Task<TipoContato> CadastrarAsync(TipoContato tipoContato)
    {
        tipoContato.IdTipoContato = Guid.NewGuid();
        await _context.TipoContatos.AddAsync(tipoContato);
        await _context.SaveChangesAsync();
        return tipoContato;
    }

    public async Task AtualizarAsync(TipoContato tipoContato)
    {
        _context.TipoContatos.Update(tipoContato);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(TipoContato tipoContato)
    {
        _context.TipoContatos.Remove(tipoContato);
        await _context.SaveChangesAsync();
    }
}
