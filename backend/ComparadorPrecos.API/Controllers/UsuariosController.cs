using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Infrastructure.Data;
using ComparadorPrecos.Core.Models;
using ComparadorPrecos.Application.DTOs;

namespace ComparadorPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    DataCadastro = u.DataCadastro
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    DataCadastro = u.DataCadastro
                })
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(CreateUsuarioDTO createUsuarioDTO)
        {
            // Verificar se email já existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == createUsuarioDTO.Email))
            {
                return BadRequest("Email já está em uso.");
            }

            var usuario = new Usuario
            {
                Nome = createUsuarioDTO.Nome,
                Email = createUsuarioDTO.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(createUsuarioDTO.Senha), // Hash da senha
                DataCadastro = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDTO = new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuarioDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UpdateUsuarioDTO updateUsuarioDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar se email já existe (excluindo o próprio usuário)
            if (await _context.Usuarios.AnyAsync(u => u.Email == updateUsuarioDTO.Email && u.Id != id))
            {
                return BadRequest("Email já está em uso.");
            }

            usuario.Nome = updateUsuarioDTO.Nome;
            usuario.Email = updateUsuarioDTO.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}