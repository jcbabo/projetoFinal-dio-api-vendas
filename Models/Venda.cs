using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_vendas.Models
{
    public class Venda
    {
        [JsonIgnore]
        public int Id { get; set; }

        // [Required(ErrorMessage = "A data da venda é obrigatória.")]
        public DateTime DataVenda { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "O número identificador do vendedor é obrigatório.")]
        public int? VendedorId { get; set; }

        [JsonIgnore]
        public virtual Vendedor? Vendedor { get; set; }

        [Required(ErrorMessage = "É necessário que tenha ao menos 1 produto para registrar a venda.")]
        public virtual List<Produto>? Itens { get; set; }
    }
}