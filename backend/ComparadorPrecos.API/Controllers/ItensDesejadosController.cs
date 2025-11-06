using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Infrastructure.Data;
using ComparadorPrecos.Core.Models;
using ComparadorPrecos.Application.DTOs;

namespace ComparadorPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensDesejadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItensDesejadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDesejadoDTO>>> GetItensDesejados()
        {
            var itens = await _context.ItensDesejados
                .Include(i => i.OpcoesCompra)
                    .ThenInclude(o => o.Produto)
                .Select(i => new ItemDesejadoDTO
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    UsuarioId = i.UsuarioId,
                    ListaComprasId = i.ListaComprasId,
                    CriadoEm = i.CriadoEm,
                    OpcoesCompra = i.OpcoesCompra.Select(o => new OpcaoCompraDTO
                    {
                        Id = o.Id,
                        ItemDesejadoId = o.ItemDesejadoId,
                        ProdutoId = o.ProdutoId,
                        Descricao = o.Descricao,
                        CriadoEm = o.CriadoEm,
                        Produto = new ProdutoDTO
                        {
                            Id = o.Produto.Id,
                            Nome = o.Produto.Nome,
                            Marca = o.Produto.Marca,
                            PrecoAtual = o.Produto.PrecoAtual,
                            Mercado = o.Produto.Mercado,
                            Url = o.Produto.Url,
                            UrlImagem = o.Produto.UrlImagem,
                            CriadoEm = o.Produto.CriadoEm,
                            AtualizadoEm = o.Produto.AtualizadoEm
                        }
                    }).ToList()
                })
                .ToListAsync();

            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDesejadoDTO>> GetItemDesejado(int id)
        {
            var item = await _context.ItensDesejados
                .Include(i => i.OpcoesCompra)
                    .ThenInclude(o => o.Produto)
                .Where(i => i.Id == id)
                .Select(i => new ItemDesejadoDTO
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    UsuarioId = i.UsuarioId,
                    ListaComprasId = i.ListaComprasId,
                    CriadoEm = i.CriadoEm,
                    OpcoesCompra = i.OpcoesCompra.Select(o => new OpcaoCompraDTO
                    {
                        Id = o.Id,
                        ItemDesejadoId = o.ItemDesejadoId,
                        ProdutoId = o.ProdutoId,
                        Descricao = o.Descricao,
                        CriadoEm = o.CriadoEm,
                        Produto = new ProdutoDTO
                        {
                            Id = o.Produto.Id,
                            Nome = o.Produto.Nome,
                            Marca = o.Produto.Marca,
                            PrecoAtual = o.Produto.PrecoAtual,
                            Mercado = o.Produto.Mercado,
                            Url = o.Produto.Url,
                            UrlImagem = o.Produto.UrlImagem,
                            CriadoEm = o.Produto.CriadoEm,
                            AtualizadoEm = o.Produto.AtualizadoEm
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDesejadoDTO>> PostItemDesejado(CreateItemDesejadoDTO createItemDTO)
        {
            var item = new ItemDesejado
            {
                Nome = createItemDTO.Nome,
                Descricao = createItemDTO.Descricao,
                UsuarioId = createItemDTO.UsuarioId,
                ListaComprasId = createItemDTO.ListaComprasId,
                CriadoEm = DateTime.UtcNow
            };

            _context.ItensDesejados.Add(item);
            await _context.SaveChangesAsync();

            var itemDTO = new ItemDesejadoDTO
            {
                Id = item.Id,
                Nome = item.Nome,
                Descricao = item.Descricao,
                UsuarioId = item.UsuarioId,
                ListaComprasId = item.ListaComprasId,
                CriadoEm = item.CriadoEm
            };

            return CreatedAtAction(nameof(GetItemDesejado), new { id = item.Id }, itemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDesejado(int id, UpdateItemDesejadoDTO updateItemDTO)
        {
            var item = await _context.ItensDesejados.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Nome = updateItemDTO.Nome;
            item.Descricao = updateItemDTO.Descricao;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDesejado(int id)
        {
            var item = await _context.ItensDesejados.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ItensDesejados.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}