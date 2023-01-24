using System.Text.Json.Serialization;

namespace api_vendas.Models.Dto
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public Venda? VendaId { get; set; }
    }
}