using FilmesMoura.WebAPI.Interfaces;
using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesMoura.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_filmeRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_filmeRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Filme novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);
                return StatusCode(201, "Filme cadastrado com sucesso!");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Filme filme)
        {
            try
            {
                _filmeRepository.AtualizarIdUrl(id, filme);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
            [HttpPut]
            public IActionResult Put(Filme filme)
            {
                try
                {
                    _filmeRepository.AtualizarIdCorpo(filme);
                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro.Message);
            }
        }
}