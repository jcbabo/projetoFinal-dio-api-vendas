using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace teste_tecnico_api_pagamentos.Models
{
    public class Vendedor
    {
        
        [Required(ErrorMessage = "O número de identificação do vendedor é obrigatório.")]
        public int Id { get; set; }

        public string? CPF { get; set; }

        [Required(ErrorMessage = "O nome e sobre são obrigatórios.")]
        public string? NomeSobrenome { get; set; }
        
        public string? Email { get; set; }
        
        public string? Telefone { get; set; }

        [JsonIgnore]
        public virtual Venda? Venda { get; set; }
    }
}