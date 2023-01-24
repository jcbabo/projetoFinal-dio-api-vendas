using System;
using System.Collections.Generic;


namespace api_vendas.Models.Dto
{
    public class VendaDTO
    {
        public VendaDTO()
        {
            this.DataVenda = DateTime.Now;
        }

        public DateTime DataVenda { get; set; }
        
        public int? VendedorId { get; set; }
        
        public List<ProdutoDTO>? Itens { get; set; }
        
        public string? Status { get; set; }
    }
}