using Moq;
using AutoFixture;
using CarrosAPI.Services;
using CarrosAPI.Interfaces.Repositories;
using CarrosAPI.Models;

namespace CarrosAPITests.Services
{
    public class CarroServiceTests
    {
        private readonly Mock<ICarrosRepository> _carrosRepositoryMock;
        private readonly CarrosService _carrosService;
        private readonly Fixture _fixture;

        public CarroServiceTests()
        {
            _fixture = new Fixture();
            _carrosRepositoryMock = new Mock<ICarrosRepository>();
            _carrosService = new CarrosService(_carrosRepositoryMock.Object);
        }


        // Testando o método "adicionar"
        [Fact]
        public async Task DadoCarroNaoExistente_QuandoAdicionarChamado_RetornarMensagemSucesso()
        {

            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync((CarrosModel?)null);

            // Act
            var resultado = await _carrosService.Adicionar(carro);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.Adicionar(carro), Times.Once());
        }

        [Fact]
        public async Task DadoCarroExistente_QuandoAdicionarChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync(carro);

            // Act e Assert 
            await Assert.ThrowsAsync<Exception>(() => _carrosService.Adicionar(carro));

            _carrosRepositoryMock.Verify(x => x.Adicionar(It.IsAny<CarrosModel>()), Times.Never());
        }

        // Testando o método "Atualizar"
        [Fact]
        public async Task DadoIdExistente_QuandoAtualizarChamado_RetornarMensagemSucesso()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.Atualizar(carro, id);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.Atualizar(carro), Times.Once());
        }

        [Fact]
        public async Task DadoIdIncorreto_QuandoAtualizar_EntaoDeveLancarExcecaoENaoChamarAtualizar()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            int idDiferente = carro.Id + 1;

            // Act
            var excecaoLancada = await Record.ExceptionAsync(() => _carrosService.Atualizar(carro, idDiferente));

            // Assert
            Assert.NotNull(excecaoLancada);
            Assert.IsType<Exception>(excecaoLancada);
            _carrosRepositoryMock.Verify(x => x.Atualizar(It.IsAny<CarrosModel>()), Times.Never());
        }

        [Fact]
        public async Task DadoIdNaoCadastrada_QuandoAtualizarChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.Atualizar(carro, id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.Atualizar(It.IsAny<CarrosModel>()), Times.Never());

        }

        // Testando o método "deletar"
        [Fact]
        public async Task DadoIdNaoEncontrado_QuandoDeletarForChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;  

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.Deletar(id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.Deletar(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async Task DadoIdEncontrado_QuandoDeletarForChamado_Deletar()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.Deletar(id);

            // Assert
            Assert.True(resultado);
            _carrosRepositoryMock.Verify(x => x.Deletar(id), Times.Once());
        }


        // Testando o método "buscar por Id"
        [Fact]
        public async Task DadoIdNaoEncontrado_QuandoBuscarPorIdForChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.BuscarPorId(id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.BuscarPorId(It.IsAny<int>()), Times.Once());
        }
        [Fact]
        public async Task DadoIdEncontrado_QuandoBuscarPorIdForChamado_RetornarCarroPorId()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorId(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.BuscarPorId(id);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.BuscarPorId(id), Times.Once());
        }

        // Testar o método buscar Todos os Carros"
        [Fact]
        public async Task DadoCarrosNaoCadastrados_QuandoBuscarTodosCarros_RetornarMensagemExcecao()
        {
            // Arrange 
            _carrosRepositoryMock.Setup(x => x.BuscarTodosCarros()).ReturnsAsync((List<CarrosModel>?)null);

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.BuscarTodosCarros());

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.BuscarTodosCarros(), Times.Once());
        }


        [Fact]
        public async Task DadoCarrosEncontrados_QuandoBuscarTodosCarrosForChamado_RetornarTodosCarros()
        {
            // Arrange
            var carros = _fixture.Create<List<CarrosModel>>();
            _carrosRepositoryMock.Setup(x => x.BuscarTodosCarros()).ReturnsAsync(carros);

            // Act
            var resultado = await _carrosService.BuscarTodosCarros();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(carros.Count, resultado.Count);
            _carrosRepositoryMock.Verify(x => x.BuscarTodosCarros(), Times.Once());
        }





    }

}







