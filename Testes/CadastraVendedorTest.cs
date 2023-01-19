using Xunit;
using System;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using teste_tecnico_api_pagamentos.Controllers;
using teste_tecnico_api_pagamentos.Repository.Interfaces;
using teste_tecnico_api_pagamentos.Models.Dto;

namespace teste_tecnico_api_pagamentos.Testes
{

    public class CadastraVendedorTest
    {
        private readonly IVendasRepository _repository = Substitute.For<IVendasRepository>();

        private readonly VendasController _controller;

        public CadastraVendedorTest()
        {
            _controller = new VendasController(_repository);
        }

        [Fact]
        public async Task CadastraVendedor_QuandoInformaçõesEstaoCorretas_RetornaOk()
        {
            // Given
            VendedorDTO Vendedor = new VendedorDTO
            {
                CPF = "10987654321",
                NomeSobrenome = "João teste mais um",
                Email = "joao@mail.com",
                Telefone = "2298765543"
            };


            // When
            var novoVendedor = new VendedorDTO
            {
                CPF = Vendedor.CPF,
                NomeSobrenome = Vendedor.NomeSobrenome,
                Email = Vendedor.Email,
                Telefone = Vendedor.Telefone
            };

            // Then
            _repository.SaveChanges().Returns(true);

            var response = await _controller.CadastraVendedor(novoVendedor);

            var Result = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, Result.StatusCode);
        }

        [Fact]
        public async Task CadastraVendedor_QuandoCpfNulo_RetornaUnauthorized()
        {
            // Given
            VendedorDTO Vendedor = new VendedorDTO
            {
                CPF = "",
                NomeSobrenome = "João teste mais um",
                Email = "joao@mail.com",
                Telefone = "2298765543"
            };


            // When
            var novoVendedor = new VendedorDTO
            {
                CPF = Vendedor.CPF,
                NomeSobrenome = Vendedor.NomeSobrenome,
                Email = Vendedor.Email,
                Telefone = Vendedor.Telefone
            };

            // Then
            _repository.SaveChanges().Returns(true);

            var response = await _controller.CadastraVendedor(novoVendedor);

            var Result = Assert.IsType<UnauthorizedObjectResult>(response);

            Assert.Equal(401, Result.StatusCode);
        }

        [Fact]
        public async Task CadastraVendedor_QuandoNomeNulo_RetornaUnauthorized()
        {
            // Given
            VendedorDTO Vendedor = new VendedorDTO
            {
                CPF = "10987654321",
                NomeSobrenome = "",
                Email = "joao@mail.com",
                Telefone = "2298765543"
            };


            // When
            var novoVendedor = new VendedorDTO
            {
                CPF = Vendedor.CPF,
                NomeSobrenome = Vendedor.NomeSobrenome,
                Email = Vendedor.Email,
                Telefone = Vendedor.Telefone
            };

            // Then
            _repository.SaveChanges().Returns(true);

            var response = await _controller.CadastraVendedor(novoVendedor);

            var result = Assert.IsType<UnauthorizedObjectResult>(response);

            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task CadastraVendedor_QuandoEmailETelefoneNulos_RetornaOk()
        {
            // Given
            VendedorDTO Vendedor = new VendedorDTO
            {
                CPF = "10987654321",
                NomeSobrenome = "João teste mais um",
                Email = "",
                Telefone = ""
            };


            // When
            var novoVendedor = new VendedorDTO
            {
                CPF = Vendedor.CPF,
                NomeSobrenome = Vendedor.NomeSobrenome,
                Email = Vendedor.Email,
                Telefone = Vendedor.Telefone
            };

            // Then
            _repository.SaveChanges().Returns(true);

            var response = await _controller.CadastraVendedor(novoVendedor);

            var okResult = Assert.IsType<OkObjectResult>(response);

            Assert.Equal(200, okResult.StatusCode);
        }
    }
}