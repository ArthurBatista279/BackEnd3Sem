using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar os tipos de usuário
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de usuário</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de usuário específico
    /// </summary>
    /// <param name="id">Id do tipo de usuário buscado</param>
    /// <returns>Status code 200 e o tipo de usuário buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            var tipoUsuario = _tipoUsuarioRepository.BuscarPorId(id);
            if (tipoUsuario == null)
                return NotFound("Tipo de usuário não encontrado.");
                
            return Ok(tipoUsuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar um tipo de usuário
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuário a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de usuário cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar um tipo de usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário a ser atualizado</param>
    /// <param name="tipoUsuario">Dados atualizados do tipo de usuário</param>
    /// <returns>Status Code 204 e o tipo de usuário atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };
            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
            return StatusCode(204, tipoUsuarioAtualizado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de deletar um tipo de usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário a ser excluído</param>
    /// <returns>Status Code 204 após a exclusão</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
