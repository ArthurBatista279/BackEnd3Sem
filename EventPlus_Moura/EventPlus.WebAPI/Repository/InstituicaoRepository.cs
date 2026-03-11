using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repository;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventoContext _context;

    public InstituicaoRepository(EventoContext context)
    {
        _context = context;
    }

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
            instituicaoBuscada.Endereco = instituicao.Endereco;
            instituicaoBuscada.Cnpj = instituicao.Cnpj;

            _context.Instituicaos.Update(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.ToList();
    }

    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.FirstOrDefault(i => i.IdInstituicao == id);
    }

    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);
        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

}