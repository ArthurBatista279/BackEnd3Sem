using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um usuario por id
    /// </summary>
    /// <param name="Id">Id do usuario a ser buscado</param>
    /// <returns>Status code 200 e o usuario buscado</returns>
    [HttpGet("{Id}")]

    public IActionResult BuscarPorId(Guid Id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(Id));
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo de cadastrar um usuario
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastra</param>
    /// <returns>Status Code 201 e o usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Email = usuario.Email!,
                Senha = usuario.Senha!,
                IdTipoUsuario = usuario.IdTipoUsuario

            };

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
