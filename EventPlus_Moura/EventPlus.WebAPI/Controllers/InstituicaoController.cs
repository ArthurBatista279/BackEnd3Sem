using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private readonly IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de listar as instituições
    /// </summary>
    /// <returns>Status code 200 e a lista de instituições</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de buscar uma instituição específica
    /// </summary>
    /// <param name="id">Id da instituição buscada</param>
    /// <returns>Status code 200 e a instituição buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de cadastrar uma instituição
    /// </summary>
    /// <param name="instituicao">Instituição a ser cadastrada</param>
    /// <returns>Status code 201 e a instituição cadastrada</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                Endereco = instituicao.Endereco!,
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!
            };
            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201, novaInstituicao);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de atualizar uma instituição
    /// </summary>
    /// <param name="id">Id da instituição a ser atualizada</param>
    /// <param name="instituicao">Dados atualizados da instituição</param>
    /// <returns>Status Code 204 e a instituição atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                Endereco = instituicao.Endereco!,
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!
            };
            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
            return StatusCode(204, instituicaoAtualizada);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// EndPoint da API que faz a chamada para o método de deletar uma instituição
    /// </summary>
    /// <param name="id">Id da instituição a ser excluída</param>
    /// <returns>Status Code 204 após a exclusão</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
