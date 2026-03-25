using ConnectPlus_Moura.Models;
using ConnectPlus_Moura.DTO;
using ConnectPlus_Moura.Interfaces;
using ConnectPlus_Moura.BdContextEvento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus_Moura.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoRepository _contatoRepository;

    /// <summary>
    /// Construtor do ContatoController com injeção de dependência do repositório.
    /// </summary>
    /// <param name="contatoRepository">Repositório de contato.</param>
    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    /// <summary>
    /// Lista todos os contatos cadastrados.
    /// </summary>
    /// <returns>Lista de contatos com seus respectivos tipos.</returns>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var contatos = await _contatoRepository.ListarAsync();
            return Ok(contatos);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Busca um contato pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do contato.</param>
    /// <returns>Contato encontrado com seu tipo, ou NotFound se não existir.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        try
        {
            var contato = await _contatoRepository.BuscarPorIdAsync(id);

            if (contato is null)
                return NotFound("Contato não encontrado.");

            return Ok(contato);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Lista todos os contatos filtrados por tipo de contato.
    /// </summary>
    /// <param name="idTipoContato">Identificador único do tipo de contato para filtro.</param>
    /// <returns>Lista de contatos do tipo especificado.</returns>
    [HttpGet("ListarPorTipoContato/{idTipoContato}")]
    public async Task<IActionResult> ListarPorTipoContato(Guid idTipoContato)
    {
        try
        {
            var contatos = await _contatoRepository.ListarPorTipoContatoAsync(idTipoContato);
            return Ok(contatos);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo contato.
    /// O campo Imagem deve receber o caminho (path) da imagem no servidor.
    /// </summary>
    /// <param name="contatoDTO">Dados do contato a ser cadastrado, incluindo o caminho da imagem (opcional).</param>
    /// <returns>Contato recém-cadastrado com status 201.</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ContatoDTO contatoDTO)
    {
        try
        {
            // Conversão manual do DTO para o Model
            var contato = new Contato
            {
                Nome = contatoDTO.Nome,
                DadosDeContato = contatoDTO.DadosDeContato,
                Imagem = contatoDTO.Imagem,
                IdTipoContato = contatoDTO.IdTipoContato
            };

            var contatoCadastrado = await _contatoRepository.CadastrarAsync(contato);
            return StatusCode(201, contatoCadastrado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Atualiza os dados de um contato existente.
    /// O campo Imagem deve receber o caminho (path) da nova imagem no servidor, ou null para manter sem imagem.
    /// </summary>
    /// <param name="id">Identificador único do contato a ser atualizado.</param>
    /// <param name="contatoDTO">Novos dados do contato, incluindo o caminho da imagem (opcional).</param>
    /// <returns>Status 204 (sem conteúdo) em caso de sucesso.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, ContatoDTO contatoDTO)
    {
        try
        {
            var contatoExistente = await _contatoRepository.BuscarPorIdAsync(id);

            if (contatoExistente is null)
                return NotFound("Contato não encontrado.");

            // Conversão manual do DTO para o Model
            contatoExistente.Nome = contatoDTO.Nome;
            contatoExistente.DadosDeContato = contatoDTO.DadosDeContato;
            contatoExistente.Imagem = contatoDTO.Imagem;
            contatoExistente.IdTipoContato = contatoDTO.IdTipoContato;

            await _contatoRepository.AtualizarAsync(contatoExistente);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Remove um contato pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador único do contato a ser removido.</param>
    /// <returns>Status 204 (sem conteúdo) em caso de sucesso.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        try
        {
            var contato = await _contatoRepository.BuscarPorIdAsync(id);

            if (contato is null)
                return NotFound("Contato não encontrado.");

            await _contatoRepository.DeletarAsync(contato);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
