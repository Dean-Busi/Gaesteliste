using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class KommentarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KommentarController(ApplicationDbContext context)
        {
            _context = context;
        }

        /////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKommentarDto kommentarDto)
        {
            var kommentarToCreate = kommentarDto.ToKommentarFromCreateDto();
            await _context.Kommentare.AddAsync(kommentarToCreate);
            await _context.SaveChangesAsync();
            return Ok("Dein Kommentar wurde gepostet.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kommentare = await _context.Kommentare.ToListAsync();
            var kommentareDto = kommentare.Select(s => s.ToKommentarDto());
            return Ok(kommentareDto);
        }

        [HttpPut]
        [Route("{inhalt}")]
        public async Task<IActionResult> Update([FromRoute] string inhalt, [FromBody] UpdateKommentarDto updateDto)
        {
            var kommentarToUpdate = await _context.Kommentare.FirstOrDefaultAsync(x => x.Inhalt == inhalt);
            var updatedKommentar = updateDto.ToKommentarFromUpdateDto();

            kommentarToUpdate.Inhalt = updatedKommentar.Inhalt;

            await _context.SaveChangesAsync();

            return Ok(kommentarToUpdate.ToKommentarDto());

        }
    }
}
