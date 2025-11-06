namespace ComparadorPrecos.Application.DTOs
{
    public class OpcaoCompraDTO
    {
        public int Id { get; set; }
        public int ItemDesejadoId { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public ProdutoDTO? Produto { get; set; }
    }

    public class CreateOpcaoCompraDTO
    {
        public int ItemDesejadoId { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }

    public class UpdateOpcaoCompraDTO
    {
        public string Descricao { get; set; } = string.Empty;
    }
}