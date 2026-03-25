using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient
        contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// Lista todos os comentários de um evento
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Status code 200 e a lista de comentários do evento</returns>
    [HttpGet("{idEvento}")]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.List(idEvento));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Lista somente os comentários visíveis (Exibe = true) de um evento
    /// </summary>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Status code 200 e a lista de comentários visíveis do evento</returns>
    [HttpGet("Exibe/{idEvento}")]
    public IActionResult ListarSomenteExibe(Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(idEvento));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Busca o comentário de um usuário em um evento específico
    /// </summary>
    /// <param name="idUsuario">Id do usuário</param>
    /// <param name="idEvento">Id do evento</param>
    /// <returns>Status code 200 e o comentário encontrado</returns>
    [HttpGet("Usuario/{idUsuario}/Evento/{idEvento}")]
    public IActionResult BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo comentário de evento, passando pelo filtro de moderação do Azure Content Safety
    /// </summary>
    /// <param name="comentarioEvento">Dados do comentário a ser cadastrado</param>
    /// <returns>Status code 201 e o comentário cadastrado</returns>
    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if(string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio");
            }
            // Criar objeto de analise 
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);
            // Chamar a API do Azure Content Safety para analisar o texto
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);
            // Verifica se o texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEvento.IdEvento,
                IdUsuario = comentarioEvento.IdUsuario,
                Descricao = comentarioEvento.Descricao,
                Exibe = !temConteudoImproprio,
                DataComentarioEvento = DateTime.Now
            };
            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Deleta um comentário de evento pelo seu Id
    /// </summary>
    /// <param name="id">Id do comentário a ser deletado</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}