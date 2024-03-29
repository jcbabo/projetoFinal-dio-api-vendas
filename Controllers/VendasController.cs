using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api_vendas.Models;
using api_vendas.Models.Dto;
using api_vendas.Repository.Interfaces;

namespace api_vendas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendasRepository _repository;

        public VendasController(IVendasRepository repository)
        {
            _repository = repository;
        }

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

                if (_produto.Id <= 0)
                {
                    return Unauthorized("O identificador do produto não pode ser nulo");
                }
                else
                {
                    _itens.Add(_produto);
                }
                
            }

            var _novaVenda = new Venda
            {
                DataVenda = novaVenda.DataVenda,
                VendedorId = novaVenda.VendedorId,
                Itens = _itens,
                Status = "aguardando pagamento"
            };

            if (_novaVenda.Itens.Count <= 0)
            {
                return Unauthorized("A venda deve conter ao menos 1 item");
            }

            // verifica se o Vendedor Id é zero ou nulo
            if (_novaVenda.VendedorId == 0 || _novaVenda.VendedorId == null)
            {
                return Unauthorized ("O campo 'VendedorId não pode ser nulo ou zero. Aponte o vendedor responsável pela venda ou cadastre-se cadastre-se como vendedor para poder registrar uma venda.");
            }
            else
            {
                try
                {
                    _repository.Add(_novaVenda);

                    return await _repository.SaveChanges() ? Ok("Venda registrada com sucesso!") : BadRequest("Erro ao salvar venda. Tente refazer a operação");
                }
                catch
                {
                    return BadRequest ("Erro ao salvar venda. Tente refazer a operação");
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

        [HttpPost("CadastraVendedor")]
        public async Task<IActionResult> CadastraVendedor(VendedorDTO vendedor)
        {
            var NomeSobrenomeFormatado = vendedor.NomeSobrenome!.ToLower();

            var _vendedor = new Vendedor
            {
                NomeSobrenome = NomeSobrenomeFormatado,
                CPF = vendedor.CPF,
                Email = vendedor.Email,
                Telefone = vendedor.Telefone
            };

            if (_vendedor.NomeSobrenome != "" && _vendedor.CPF != "")
            {
                _repository.Add(_vendedor);

                var okResult = await _repository.SaveChanges();

                return Ok("Vendedor cadastrado com sucesso");
            }
            else 
            {
                return Unauthorized("Pelo menos o Nome e CPF do vendedor devem ser preenchidos");
            }
        }

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
                return BadRequest("Status Permitidos para atualização: 'pagamento aprovado', 'enviado para transportadora', 'entregue', 'cancelada'");
            }

            if (vendaEfetuada.Status == ListaDeStatus[2])
            {
                return BadRequest("Alerta: Essa venda já foi entregue");
            }
            else if (vendaEfetuada.Status == ListaDeStatus[3])
            {
                return BadRequest("Alerta: Essa venda foi cancelada.");
            }

            if (vendaEfetuada.Status == "aguardando pagamento")
            {
                if (statusVendaAtualizada != ListaDeStatus[0] && statusVendaAtualizada != ListaDeStatus[3])
                    return BadRequest("Status atual: 'Aguardando pagamento'. Atualizações permitidas: 'pagamento aprovado' ou 'cancelada'");
            }
            else if (vendaEfetuada.Status == ListaDeStatus[0])
            {
                if (statusVendaAtualizada != ListaDeStatus[1] && statusVendaAtualizada != ListaDeStatus[3])
                    return BadRequest("Status atual: 'Pagamento aprovado'. Atualizações permitidas: 'enviado para a transportadora' ou 'cancelada'");
            }
            else if (vendaEfetuada.Status == ListaDeStatus[1])
            {
                if (statusVendaAtualizada != ListaDeStatus[2])
                    return BadRequest("Status atual: 'Enviado para a transportadora'. Atualização permitida: 'entregue'");
            }
            
            vendaEfetuada.Status = statusVendaAtualizada;

            _repository.Update(vendaEfetuada);

            return await _repository.SaveChanges() ? Ok("Venda atualizada com sucesso!") : BadRequest("Erro ao atualizar venda. Tente novamente.");
        }
    }
}