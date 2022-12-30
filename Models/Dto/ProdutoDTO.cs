using System.Text.Json.Serialization;

namespace teste_tecnico_api_pagamentos.Models.Dto
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public Venda? VendaId { get; set; }
    }
}