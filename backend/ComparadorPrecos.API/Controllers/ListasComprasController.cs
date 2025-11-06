using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Infrastructure.Data;
using ComparadorPrecos.Core.Models;
using ComparadorPrecos.Application.DTOs;

namespace ComparadorPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListasComprasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ListasComprasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaComprasDTO>>> GetListasCompras()
        {
            var listas = await _context.ListasCompras
                .Include(l => l.ItensDesejados)
                .Select(l => new ListaComprasDTO
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    UsuarioId = l.UsuarioId,
                    CriadaEm = l.CriadaEm,
                    ItensDesejados = l.ItensDesejados.Select(i => new ItemDesejadoDTO
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        Descricao = i.Descricao,
                        UsuarioId = i.UsuarioId,
                        ListaComprasId = i.ListaComprasId,
                        CriadoEm = i.CriadoEm
                    }).ToList()
                })
                .ToListAsync();

            return Ok(listas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListaComprasDTO>> GetListaCompras(int id)
        {
            var lista = await _context.ListasCompras
                .Include(l => l.ItensDesejados)
                .Where(l => l.Id == id)
                .Select(l => new ListaComprasDTO
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    UsuarioId = l.UsuarioId,
                    CriadaEm = l.CriadaEm,
                    ItensDesejados = l.ItensDesejados.Select(i => new ItemDesejadoDTO
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        Descricao = i.Descricao,
                        UsuarioId = i.UsuarioId,
                        ListaComprasId = i.ListaComprasId,
                        CriadoEm = i.CriadoEm
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (lista == null)
            {
                return NotFound();
            }

            return lista;
        }

        [HttpPost]
        public async Task<ActionResult<ListaComprasDTO>> PostListaCompras(CreateListaComprasDTO createListaDTO)
        {
            var lista = new ListaCompras
            {
                Nome = createListaDTO.Nome,
                UsuarioId = createListaDTO.UsuarioId,
                CriadaEm = DateTime.UtcNow
            };

            _context.ListasCompras.Add(lista);
            await _context.SaveChangesAsync();

            var listaDTO = new ListaComprasDTO
            {
                Id = lista.Id,
                Nome = lista.Nome,
                UsuarioId = lista.UsuarioId,
                CriadaEm = lista.CriadaEm
            };

            return CreatedAtAction(nameof(GetListaCompras), new { id = lista.Id }, listaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutListaCompras(int id, UpdateListaComprasDTO updateListaDTO)
        {
            var lista = await _context.ListasCompras.FindAsync(id);
            if (lista == null)
            {
                return NotFound();
            }

            lista.Nome = updateListaDTO.Nome;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListaCompras(int id)
        {
            var lista = await _context.ListasCompras.FindAsync(id);
            if (lista == null)
            {
                return NotFound();
            }

            _context.ListasCompras.Remove(lista);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}