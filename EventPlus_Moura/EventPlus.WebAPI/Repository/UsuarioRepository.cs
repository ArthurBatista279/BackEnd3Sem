using EventPlus.WebAPI.BdContextEvento;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventoContext _context;

    public UsuarioRepository(EventoContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Busca o usuario pelo e-mail e valida o hash da senha
    /// </summary>
    /// <param name="Email">email do usuario</param>
    /// <param name="Senha">senha do usuario</param>
    /// <returns>Usuario buscado e validado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //Primeiro, buscamos o usußrio pelo email
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

        if (usuarioBuscado != null)
        {
            //Comparamos o hash da senha informada com o hash armazenado no banco de dados
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
        }

        return null!;
    }
    /// <summary>
    /// Busca um usuario pelo id, incluindo os dados do tipo usuario, utilizando o mÚtodo Include para realizar o carregamento dos dados relacionados
    /// </summary>
    /// <param name="IdUsuario">╠d do usuario a ser buscado</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
    }
    /// <summary>
    /// Cadastra um novo usuario com a senha criptografia
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        // Validação 1: Verificar se o IdTipoUsuario existe
        var tipoUsuarioExiste = _context.TipoUsuarios.Any(tu => tu.IdTipoUsuario == usuario.IdTipoUsuario);
        if (!tipoUsuarioExiste)
        {
            throw new Exception("O Tipo de Usuário informado não foi encontrado.");
        }

        // Validação 2: Verificar se já existe um usuário com o mesmo Email
        var emailExiste = _context.Usuarios.Any(u => u.Email == usuario.Email);
        if (emailExiste)
        {
            throw new Exception("Este e-mail já está em uso por outro usuário.");
        }

        // Validação 3: Verificar se já existe um usuário com o mesmo Nome
        var nomeExiste = _context.Usuarios.Any(u => u.Nome == usuario.Nome);
        if (nomeExiste)
        {
            throw new Exception("Este nome de usuário já está em uso.");
        }

        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}
