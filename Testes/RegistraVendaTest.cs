using Xunit;
using System;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using teste_tecnico_api_pagamentos.Controllers;
using teste_tecnico_api_pagamentos.Repository.Interfaces;
using teste_tecnico_api_pagamentos.Models.Dto;

namespace teste_tecnico_api_pagamentos.Testes
{

    public class RegistraVendaTest
    {
        private readonly IVendasRepository _repository = Substitute.For<IVendasRepository>();

        private readonly VendasController _controller;

        public RegistraVendaTest()
        {
            _controller = new VendasController(_repository);
        }

        [Fact]
        public async Task RegistraVenda_QuandoVendaEstaCorreta_RetornaOk()
        {
            // Given
            ProdutoDTO novoProduto = new ProdutoDTO
            {
                Id = 010102,
                Nome = "Produto teste 123",
            };
            

            List<ProdutoDTO> _itens = new List<ProdutoDTO>()
            {
                novoProduto
            };

            // When
            var novaVenda = new VendaDTO
            {
                DataVenda = DateTime.Now,
                VendedorId = 6,
                Itens = _itens,
                Status = "Aguardando pagamento"
            };

            // Then
            _repository.SaveChanges().Returns(true);
            
            var response = await _controller.RegistraVenda(novaVenda);
            
            var okResult = Assert.IsType<OkObjectResult>(response);
            
            Assert.Equal(200, okResult.StatusCode);
        }
    

        [Fact]
        public async Task RegistraVenda_QuandoVendedorEhZeroOuNulo_RetornaUnauthorized()
        {
            // Given
            ProdutoDTO novoProduto = new ProdutoDTO
            {
                Id = 010102,
                Nome = "Produto teste 123",
            };


            List<ProdutoDTO> _itens = new List<ProdutoDTO>()
            {
                novoProduto
            };

            // When
            var novaVenda = new VendaDTO
            {
                DataVenda = DateTime.Now,
                VendedorId = 0,
                Itens = _itens,
                Status = "Aguardando pagamento"
            };

            // Then
            var response = await _controller.RegistraVenda(novaVenda);

            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(response);

            Assert.Equal(401, unauthorized.StatusCode);
        }


        [Fact]
        public async Task RegistraVenda_QuandoNaoHaItens_RetornaUnauthorized()
        {
            // Given
            ProdutoDTO novoProduto = new ProdutoDTO
            {

            };

            List<ProdutoDTO> _itens = new List<ProdutoDTO>()
            {
                novoProduto
            };

            // When
            var novaVenda = new VendaDTO
            {
                DataVenda = DateTime.Now,
                VendedorId = 6,
                Itens = _itens,
                Status = "Aguardando pagamento"
            };

            // Then
            var response = await _controller.RegistraVenda(novaVenda);

            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(response);

            Assert.Equal(401, unauthorized.StatusCode);
        }

        [Fact]
        public async Task RegistraVenda_QuandoIdProdutoEhZero_RetornaUnauthorized()
        {
            // Given
            ProdutoDTO novoProduto = new ProdutoDTO
            {
                Id = 0,
                Nome = "Produto teste 123",
            };


            List<ProdutoDTO> _itens = new List<ProdutoDTO>()
            {
                novoProduto
            };

            // When
            var novaVenda = new VendaDTO
            {
                DataVenda = DateTime.Now,
                VendedorId = 6,
                Itens = _itens,
                Status = "Aguardando pagamento"
            };

            // Then
            var response = await _controller.RegistraVenda(novaVenda);

            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(response);

            Assert.Equal(401, unauthorized.StatusCode);
        }
    }
}