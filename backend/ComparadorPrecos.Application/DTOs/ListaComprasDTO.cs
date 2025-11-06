namespace ComparadorPrecos.Application.DTOs
{
    public class ListaComprasDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public DateTime CriadaEm { get; set; }
        public List<ItemDesejadoDTO> ItensDesejados { get; set; } = new();
    }

    public class CreateListaComprasDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }

    public class UpdateListaComprasDTO
    {
        public string Nome { get; set; } = string.Empty;
    }
}