using System;
using System.Collections.Generic;

namespace teste_tecnico_api_pagamentos.Controllers
{
    public class VendaDTO
    {
        public int? Id { get; set; }
        public DateTime DataVenda { get; set; }
        public int? VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }
        public List<Produto>? Itens { get; set; }
        public string? Status { get; set; }
    }

    public class Produto
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
    }

    public class Vendedor
    {
        public int? Id { get; set; }
        public string? CPF { get; set; }
        public string? NomeSobrenome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}