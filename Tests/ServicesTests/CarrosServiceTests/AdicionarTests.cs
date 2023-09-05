using Xunit;
using Moq;
using AutoFixture;
using CarrosAPI.Repository;
using CarrosAPI.Services;

namespace CarrosAPI.Tests.ServicesTests.CarrosServiceTests
{
    public class AdicionarTests
    {
        [Fact]
        public void DadoCarroNaoExistente_QuandoAdicionarChamado_RetornarMensagemSucesso()
        {
            // Arrange
            var carroNaoExistente = new Fixture().Create<Adicionar>();  

            // Act

            // Assert 
        }

        [Fact]
        public void DadoCarroExistente_QuandoAdicionarChamado_RetornarMensagemExcecao()
        {
            // Arrange

            // Act

            // Assert 
        }
    }
} 