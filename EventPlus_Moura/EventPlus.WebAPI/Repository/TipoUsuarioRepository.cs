using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.BdContextEvento;

namespace EventPlus.WebAPI.Repository;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventoContext _context;

    public TipoUsuarioRepository(EventoContext context)
    {
        _context = context;
    }

    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

            _context.TipoUsuarios.Update(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.ToList();
    }

    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id);
    }

    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

}