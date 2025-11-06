namespace ComparadorPrecos.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        //public List<ListaComprasDTO> ListasCompras { get; set; } = new();
        //public List<ItemDesejadoDTO> ItensDesejados { get; set; } = new();
    }

    public class CreateUsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class UpdateUsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}