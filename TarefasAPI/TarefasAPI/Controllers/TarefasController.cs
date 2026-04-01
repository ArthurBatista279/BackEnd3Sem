using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TarefasAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly Interfaces.ITarefasRepositories _tarefasRepositories;

    public TarefasController(Interfaces.ITarefasRepositories tarefasRepositories)
    {
        _tarefasRepositories = tarefasRepositories;
    }

    [HttpPost]
    public IActionResult Cadastrar(DTO.TarefasDTO tarefas)
    {
            try
            {
               var tarefasModel = new Models.Tarefa
                {
                    Titulo = tarefas.Nome!,
                    Descricao = tarefas.Descricao!,
                    StatusDeConclusao = tarefas.StatusDeConclusao
                };

            _tarefasRepositories.Cadastrar(tarefasModel);
                return StatusCode(201, tarefasModel);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tarefasRepositories.Listar());
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tarefasRepositories.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, Models.Tarefa tarefas)
    {
        try
        {
            _tarefasRepositories.Atualizar(id, tarefas);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tarefasRepositories.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}