namespace ComparadorPrecos.Application.DTOs
{
    public class ItemDesejadoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int UsuarioId { get; set; }
        public int ListaComprasId { get; set; }
        public DateTime CriadoEm { get; set; }
        public List<OpcaoCompraDTO> OpcoesCompra { get; set; } = new();
    }

    public class CreateItemDesejadoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int UsuarioId { get; set; }
        public int ListaComprasId { get; set; }
    }

    public class UpdateItemDesejadoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
    }
}