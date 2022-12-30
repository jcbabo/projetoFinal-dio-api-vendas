using System;
using System.Collections.Generic;


namespace teste_tecnico_api_pagamentos.Models.Dto
{
    public class VendaDTO
    {
        public VendaDTO()
        {
            this.DataVenda = DateTime.Now;
        }

        public DateTime DataVenda { get; set; }
        
        public int? VendedorId { get; set; }
        
        public List<Produto>? Itens { get; set; }
        
        public string? Status { get; set; }
    }
}