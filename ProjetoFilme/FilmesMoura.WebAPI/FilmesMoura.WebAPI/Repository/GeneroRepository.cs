using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.Interfaces;
using FilmesMoura.WebAPI.Models;

namespace FilmesMoura.WebAPI.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly FilmeContext _context;
        public GeneroRepository(FilmeContext context)
        {
            _context = context;
        }
        public void AtualizarIdCorpo(Genero GeneroAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(Guid id, Genero novoGenero)
        {

            throw new NotImplementedException();
        }
        public Genero BuscarPorId(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Generos.Find(id.ToString());
                return generoBuscado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Cadastrar(Genero NovoGenero)
        {
            try
            {
                NovoGenero.IdGenero = Guid.NewGuid().ToString();
                _context.Generos.Add(NovoGenero);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Genero> Listar()
        {

            try
            {
                List<Genero> listaGeneros = _context.Generos.ToList();  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
             return _context.Generos.ToList();
        }
    }
}