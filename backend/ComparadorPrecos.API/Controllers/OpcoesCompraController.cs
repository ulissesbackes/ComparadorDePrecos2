using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Infrastructure.Data;
using ComparadorPrecos.Core.Models;
using ComparadorPrecos.Application.DTOs;

namespace ComparadorPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcoesCompraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OpcoesCompraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpcaoCompraDTO>>> GetOpcoesCompra()
        {
            var opcoes = await _context.OpcoesCompra
                .Include(o => o.Produto)
                .Select(o => new OpcaoCompraDTO
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
                })
                .ToListAsync();

            return Ok(opcoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpcaoCompraDTO>> GetOpcaoCompra(int id)
        {
            var opcao = await _context.OpcoesCompra
                .Include(o => o.Produto)
                .Where(o => o.Id == id)
                .Select(o => new OpcaoCompraDTO
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
                })
                .FirstOrDefaultAsync();

            if (opcao == null)
            {
                return NotFound();
            }

            return opcao;
        }

        [HttpPost]
        public async Task<ActionResult<OpcaoCompraDTO>> PostOpcaoCompra(CreateOpcaoCompraDTO createOpcaoDTO)
        {
            var opcao = new OpcaoCompra
            {
                ItemDesejadoId = createOpcaoDTO.ItemDesejadoId,
                ProdutoId = createOpcaoDTO.ProdutoId,
                Descricao = createOpcaoDTO.Descricao,
                CriadoEm = DateTime.UtcNow
            };

            _context.OpcoesCompra.Add(opcao);
            await _context.SaveChangesAsync();

            var opcaoDTO = new OpcaoCompraDTO
            {
                Id = opcao.Id,
                ItemDesejadoId = opcao.ItemDesejadoId,
                ProdutoId = opcao.ProdutoId,
                Descricao = opcao.Descricao,
                CriadoEm = opcao.CriadoEm
            };

            return CreatedAtAction(nameof(GetOpcaoCompra), new { id = opcao.Id }, opcaoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpcaoCompra(int id, UpdateOpcaoCompraDTO updateOpcaoDTO)
        {
            var opcao = await _context.OpcoesCompra.FindAsync(id);
            if (opcao == null)
            {
                return NotFound();
            }

            opcao.Descricao = updateOpcaoDTO.Descricao;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpcaoCompra(int id)
        {
            var opcao = await _context.OpcoesCompra.FindAsync(id);
            if (opcao == null)
            {
                return NotFound();
            }

            _context.OpcoesCompra.Remove(opcao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("por-item/{itemDesejadoId}")]
        public async Task<ActionResult<IEnumerable<OpcaoCompraDTO>>> GetOpcoesPorItemDesejado(int itemDesejadoId)
        {
            var opcoes = await _context.OpcoesCompra
                .Include(o => o.Produto)
                .Where(o => o.ItemDesejadoId == itemDesejadoId)
                .Select(o => new OpcaoCompraDTO
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
                })
                .ToListAsync();

            return Ok(opcoes);
        }
    }
}