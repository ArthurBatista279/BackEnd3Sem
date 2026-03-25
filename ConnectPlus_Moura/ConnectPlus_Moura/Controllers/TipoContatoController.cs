using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus_Moura.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private readonly ITipoContatoRepository _tipoContatoRepository;

    /// <summary>
    /// Construtor do TipoContatoController com injeção de dependência do repositório.
    /// </summary>
    /// <param name="tipoContatoRepository">Repositório de tipo de contato.</param>
    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }

    /// <summary>
    /// Lista todos os tipos de contato cadastrados.
    /// </summary>
    /// <returns>Lista de tipos de contato.</returns>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var tiposContato = await _tipoContatoRepository.ListarAsync();
            return Ok(tiposContato);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Busca um tipo de contato pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do tipo de contato.</param>
    /// <returns>Tipo de contato encontrado ou NotFound se não existir.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        try
        {
            var tipoContato = await _tipoContatoRepository.BuscarPorIdAsync(id);

            if (tipoContato is null)
                return NotFound("Tipo de contato não encontrado.");

            return Ok(tipoContato);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo tipo de contato.
    /// </summary>
    /// <param name="tipoContatoDTO">Dados do tipo de contato a ser cadastrado.</param>
    /// <returns>Tipo de contato recém-cadastrado com status 201.</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            // Conversão manual do DTO para o Model
            var tipoContato = new TipoContato
            {
                Titulo = tipoContatoDTO.Titulo
            };

            var tipoContatoCadastrado = await _tipoContatoRepository.CadastrarAsync(tipoContato);
            return StatusCode(201, tipoContatoCadastrado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Atualiza os dados de um tipo de contato existente.
    /// </summary>
    /// <param name="id">Identificador único do tipo de contato a ser atualizado.</param>
    /// <param name="tipoContatoDTO">Novos dados do tipo de contato.</param>
    /// <returns>Status 204 (sem conteúdo) em caso de sucesso.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, TipoContatoDTO tipoContatoDTO)
    {
        try
        {
            var tipoContatoExistente = await _tipoContatoRepository.BuscarPorIdAsync(id);

            if (tipoContatoExistente is null)
                return NotFound("Tipo de contato não encontrado.");

            // Conversão manual do DTO para o Model
            tipoContatoExistente.Titulo = tipoContatoDTO.Titulo;

            await _tipoContatoRepository.AtualizarAsync(tipoContatoExistente);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Remove um tipo de contato pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do tipo de contato a ser removido.</param>
    /// <returns>Status 204 (sem conteúdo) em caso de sucesso.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        try
        {
            var tipoContato = await _tipoContatoRepository.BuscarPorIdAsync(id);

            if (tipoContato is null)
                return NotFound("Tipo de contato não encontrado.");

            await _tipoContatoRepository.DeletarAsync(tipoContato);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
