using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Infrastructure.Data;
using ComparadorPrecos.Core.Models;
using ComparadorPrecos.Application.DTOs;

namespace ComparadorPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtos = await _context.Produtos
                .Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Marca = p.Marca,
                    PrecoAtual = p.PrecoAtual,
                    Mercado = p.Mercado,
                    Url = p.Url,
                    UrlImagem = p.UrlImagem,
                    CriadoEm = p.CriadoEm,
                    AtualizadoEm = p.AtualizadoEm
                })
                .ToListAsync();

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var produto = await _context.Produtos
                .Where(p => p.Id == id)
                .Select(p => new ProdutoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Marca = p.Marca,
                    PrecoAtual = p.PrecoAtual,
                    Mercado = p.Mercado,
                    Url = p.Url,
                    UrlImagem = p.UrlImagem,
                    CriadoEm = p.CriadoEm,
                    AtualizadoEm = p.AtualizadoEm
                })
                .FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(CreateProdutoDTO createProdutoDTO)
        {
            var produto = new Produto
            {
                Nome = createProdutoDTO.Nome,
                Marca = createProdutoDTO.Marca,
                PrecoAtual = createProdutoDTO.PrecoAtual,
                Mercado = createProdutoDTO.Mercado,
                Url = createProdutoDTO.Url,
                UrlImagem = createProdutoDTO.UrlImagem,
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDTO = new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Marca = produto.Marca,
                PrecoAtual = produto.PrecoAtual,
                Mercado = produto.Mercado,
                Url = produto.Url,
                UrlImagem = produto.UrlImagem,
                CriadoEm = produto.CriadoEm,
                AtualizadoEm = produto.AtualizadoEm
            };

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produtoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, UpdateProdutoDTO updateProdutoDTO)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.Nome = updateProdutoDTO.Nome;
            produto.Marca = updateProdutoDTO.Marca;
            produto.PrecoAtual = updateProdutoDTO.PrecoAtual;
            produto.Mercado = updateProdutoDTO.Mercado;
            produto.Url = updateProdutoDTO.Url;
            produto.UrlImagem = updateProdutoDTO.UrlImagem;
            produto.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}