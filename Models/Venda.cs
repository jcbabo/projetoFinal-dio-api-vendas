using System;
using System.Collections.Generic;

namespace teste_tecnico_api_pagamentos.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int VendedorId { get; set; }
        public string? Status { get; set; }
        public Vendedor? Vendedor { get; set; }
        public List<Produto>? Itens { get; set; }
    }
}