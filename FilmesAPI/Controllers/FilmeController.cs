using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filmes.Add(filme);
            filme.Id = id++;
        }

        [HttpGet]
        public IEnumerable<Filme> BuscaFilmes()
        {
            return filmes;
        }

        [HttpGet("{id}")]
        public Filme buscaFilmePorId(int id){

            return filmes.FirstOrDefault(filme => filme.Id == id);
        }
    }
}
