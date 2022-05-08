using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.Id }, filme);
        }


        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.Filmes;
        }


        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {  
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDTO filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
                return Ok(filmeDTO); 
            }
            return NotFound();
        }


        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
