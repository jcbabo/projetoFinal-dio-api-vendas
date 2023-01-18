using System.Collections.Generic;
using System.Threading.Tasks;
using teste_tecnico_api_pagamentos.Context;
using Microsoft.AspNetCore.Mvc;
using teste_tecnico_api_pagamentos.Models;
using teste_tecnico_api_pagamentos.Models.Dto;
using teste_tecnico_api_pagamentos.Repository.Interfaces;

namespace teste_tecnico_api_pagamentos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendasRepository _repository;
        private readonly VendaContext _context;

        public VendasController(IVendasRepository repository, VendaContext context)
        {
            _repository = repository;
            _context = context;
        }

        // TODO
        // Verificar por que a data não foi enviada e consta null no banco
        [HttpPost("RegistraVenda")]
        public async Task<IActionResult> RegistraVenda(VendaDTO novaVenda)
        {

            List<Produto> _itens = new List<Produto>()
            {

            };

            foreach (var item in novaVenda.Itens!)
            {
                Produto _produto = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                };
                _itens.Add(_produto);
            }

            var _novaVenda = new Venda
            {
                DataVenda = novaVenda.DataVenda,
                VendedorId = novaVenda.VendedorId,
                Itens = _itens,
                Status = "aguardando pagamento"
            };

            // verifica se o Vendedor Id é zero ou nulo
            if (_novaVenda.VendedorId == 0 || _novaVenda.VendedorId == null)
            {
                return Unauthorized (new {Erro = "O campo 'VendedorId não pode ser nulo ou zero. Aponte o vendedor responsável pela venda."});
            }
            else
            {
                try
                {
                    _repository.Add(_novaVenda);

                    return await _repository.SaveChanges() ? Ok("Venda registrada com sucesso!") : BadRequest(new { Erro = "Erro ao salvar venda. Tente refazer a operação" });
                }
                catch
                {
                    return BadRequest (new {Erro = "Erro ao salvar venda. Tente refazer a operação" });
                }
            }      
        }

        [HttpGet("BuscaVendaPorId")]
        public async Task<IActionResult> GetVendaById(int id)
        {
            var venda = await _repository.GetVendaById(id);

            if (venda != null)
                return Ok(venda);
            else
                return NotFound("Venda não encontrada!");
        }

        // [HttpPost("CadastraVendedor")]
        // public async Task<IActionResult> CadastraVendedor(VendedorDTO vendedor)
        // {
        //     var NomeSobrenomeFormatado = vendedor.NomeSobrenome!.ToLower();

        //     var _vendedor = new Vendedor
        //     {
        //         NomeSobrenome = NomeSobrenomeFormatado,
        //         CPF = vendedor.CPF,
        //         Email = vendedor.Email,
        //         Telefone = vendedor.Telefone
        //     };

        //     _context.Add(_vendedor);

        //     return await _repository.SaveChanges() ? Ok("Vendedor cadastrado com sucesso") : BadRequest("Erro ao cadastrar o vendedor. Tente novamente.");
        // }

        
        // [HttpGet("ListaVendedores")]
        // public IEnumerable<Vendedor> ListaVendedores()
        // {
        //     return _context.Vendedor!;
        // }


        // TODO
        // é preciso iterar pelas vendas para verificar se contém o id do parâmetro e assim armazenar numa lista. Depois retornar essa lista de vendas
        // // [HttpGet("BuscaVendasPorVendedorId")]
        // // public IActionResult GetVendasPorVendedorById(int vendedorId)
        // // {
        // //     try
        // //     {
        // //         var listaDeVendas = _context.InfoVendas
        // //         .Include(x => x.Vendedor)
        // //         .Include(x => x.Itens)
        // //         .Where(x => x.VendedorId == vendedorId).ToList();

        // //         return Ok(listaDeVendas);
        // //     }
        // //     catch
        // //     {
        // //         return BadRequest(new { Erro = "Não foram encontradas vendas para este vendedor. Confira o identificador do vendedor"});
        // //     }
        // // }

        [HttpPut("AtualizaStatusVenda")]
        public async Task<IActionResult> AtualizaStatusVenda(int id, string status)
        {
            List<string> ListaDeStatus = new List<string>()
            {
                "pagamento aprovado",
                "enviado para transportadora",
                "entregue",
                "cancelada"
            };

            // busca a venda no banco e verifica seu status
            var vendaEfetuada = await _repository.GetVendaById(id);

            // pega o status novo e converte pra minúsculas
            var statusVendaAtualizada = status.ToLower();
            
            if ((!ListaDeStatus.Contains(statusVendaAtualizada)) && statusVendaAtualizada == "aguardando pagamento")
            {
                return BadRequest( new { Erro = "Status Permitidos para atualização: 'pagamento aprovado', 'enviado para transportadora', 'entregue', 'cancelada'" });
            }

            if (vendaEfetuada.Status == ListaDeStatus[2])
            {
                return BadRequest(new { Erro = "Alerta: Essa venda já foi entregue" });
            }
            else if (vendaEfetuada.Status == ListaDeStatus[3])
            {
                return BadRequest(new { Erro = "Alerta: Essa venda foi cancelada." });
            }

            if (vendaEfetuada.Status == "aguardando pagamento")
            {
                if (statusVendaAtualizada != ListaDeStatus[0] && statusVendaAtualizada != ListaDeStatus[3])
                    return BadRequest( new { Erro = "Status atual: 'Aguardando pagamento'. Atualizações permitidas: 'pagamento aprovado' ou 'cancelada'" });
            }
            else if (vendaEfetuada.Status == ListaDeStatus[0])
            {
                if (statusVendaAtualizada != ListaDeStatus[1] && statusVendaAtualizada != ListaDeStatus[3])
                    return BadRequest(new { Erro = "Status atual: 'Pagamento aprovado'. Atualizações permitidas: 'enviado para a transportadora' ou 'cancelada'" });
            }
            else if (vendaEfetuada.Status == ListaDeStatus[1])
            {
                if (statusVendaAtualizada != ListaDeStatus[2])
                    return BadRequest(new { Erro = "Status atual: 'Enviado para a transportadora'. Atualização permitida: 'entregue'" });
            }
            
            vendaEfetuada.Status = statusVendaAtualizada;

            _repository.Update(vendaEfetuada);

            return await _repository.SaveChanges() ? Ok("Venda atualizada com sucesso!") : BadRequest("Erro ao atualizar venda. Tente novamente.");
        }
    }
}