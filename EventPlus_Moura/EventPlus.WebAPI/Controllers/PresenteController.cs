using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresenteController : ControllerBase
{
    private readonly IPresencaRepository _presenteRepository;

    public PresenteController(IPresencaRepository presenteRepository)
    {
        _presenteRepository = presenteRepository; 
    }

    /// <summary>
    /// EndPoint da API que retorna uma lista de presença de um usuario especifico
    /// </summary>
    /// <param name="idUsuario">Id do usuario para filtragem</param>
    /// <returns>Status Code 200 e uma lista de presença</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult ListarMinhas(Guid idUsuario) // Correção: O parâmetro agora é idUsuario
    {
        try
        {
            return Ok(_presenteRepository.ListarMinhas(idUsuario));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que lista as presenças de um evento especifico
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Status Code 200 e uma lista de presença</returns>
    [HttpGet("{idEvento}")]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_presenteRepository.Listar(idEvento));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que busca uma presença especifica por Id
    /// </summary>
    /// <param name="id">Id da presença buscada</param>
    /// <returns>Status Code 200 e a presença buscada</returns>
    [HttpGet("BuscarPorId/{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presenteRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API para inscrever um usuário em um evento (cadastrar presença)
    /// </summary>
    /// <param name="presenca">Objeto DTO de presença</param>
    /// <returns>Status Code 201 e a nova presença</returns>
    [HttpPost]
    public IActionResult Inscrever(PresenteDTO presenca)
    {
        try
        {
            var novaPresenca = new Presente
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario
            };

            _presenteRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar uma presença
    /// </summary>
    /// <param name="id">Id da presença a ser atualizada</param>
    /// <param name="presenca">DTO com os novos dados</param>
    /// <returns>Status Code 204 e a presença atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresenteDTO presenca)
    {
        try
        {
            var presencaAtualizada = new Presente
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario
            };

            _presenteRepository.Atualizar(id, presencaAtualizada);
            return StatusCode(204, presencaAtualizada);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de deletar uma presença
    /// </summary>
    /// <param name="id">Id da presença a ser excluída</param>
    /// <returns>Status Code 204 (No Content)</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            // Nota: Se a sua interface pedir (id, presenca) em vez de só (id), 
            // você precisa atualizar a interface para pedir apenas o Guid id, que é o padrão!
            _presenteRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}