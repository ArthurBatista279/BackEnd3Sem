using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models; // Adicionado para referenciar a model Evento
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar todos os eventos
    /// </summary>
    /// <returns>Status code 200 e a lista de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um evento especifico
    /// </summary>
    /// <param name="id">Id do evento buscado</param>
    /// <returns>Status code 200 e o evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_eventoRepository.buscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar eventos filtrado pelo usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Status code 200 e a Lista de eventos filtrados por usuario</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorID(IdUsuario));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar os próximos eventos
    /// </summary>
    /// <returns>Status code 200 e uma lista de proximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar um evento
    /// </summary>
    /// <param name="evento">Objeto evento a ser cadastrado</param>
    /// <returns>Status code 201 e o evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(Evento evento)
    {
        try
        {
            _eventoRepository.Cadastrar(evento);
            return StatusCode(201, evento);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar um evento
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="evento">Objeto evento com os novos dados</param>
    /// <returns>Status Code 204</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, Evento evento)
    {
        try
        {
            _eventoRepository.Atualizar(id, evento);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para a método de deletar um evento
    /// </summary>
    /// <param name="id">Id do evento a ser excluído</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}