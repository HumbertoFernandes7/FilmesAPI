using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = new Filme();

            filme.Titulo = filmeDto.Titulo;
            filme.Duracao = filmeDto.Duracao;

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(buscaFilmePorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> BuscaFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult buscaFilmePorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id,[FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filmeEncontrado == null)
            {
                return NotFound();
            }
            Filme filme = new Filme();

            filmeEncontrado.Titulo = filmeDto.Titulo;
            filmeEncontrado.Duracao = filmeDto.Duracao;
            _context.SaveChanges();

            return Ok(filmeEncontrado);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filmeEncontrado = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filmeEncontrado == null)
            {
                return NotFound();
            }
            _context.Remove(filmeEncontrado);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
