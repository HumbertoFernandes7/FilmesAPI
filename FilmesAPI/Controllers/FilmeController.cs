﻿using AutoMapper;
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
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
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
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
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
            _mapper.Map(filmeDto, filmeEncontrado);
            _context.SaveChanges();
            return NoContent();
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
