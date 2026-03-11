using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private readonly ITipoEventoRepository _tipoEventoRepository;
    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de lista os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e alista de tipo de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de evento especifico
    /// </summary>
    /// <param name="id">Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e o tipo evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar um tipo de cadastro um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">tipo de evento a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo
            };
            _tipoEventoRepository.Cadastrar(novoTipoEvento);

            return StatusCode(201, novoTipoEvento);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo evento com os dados atualizados</param>
    /// <returns>Status Code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento)
    {
        try
        {
            var tipoEventoAtualizado = new TipoEvento
            {
                Titulo = tipoEvento.Titulo
            };
            _tipoEventoRepository.Atualizar(id, tipoEventoAtualizado);
            return StatusCode(204, tipoEventoAtualizado);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para a método de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo do evento a ser excluído</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}