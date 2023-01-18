using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace teste_tecnico_api_pagamentos.Models
{
    public class Produto
    {
        
        [Required(ErrorMessage = "O número identificador do produto é obrigatório.")]
        public int Id { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public virtual int? VendaId { get; set; }

        [JsonIgnore]
        public virtual Venda? Venda { get; }
    }
}