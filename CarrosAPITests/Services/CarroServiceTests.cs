using Moq;
using AutoFixture;
using CarrosAPI.Core.Models;
using CarrosAPI.Core.Services;
using CarrosAPI.Core.Interfaces.Repositories;

namespace CarrosAPI.Tests.Services
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


        [Fact(DisplayName = "Adiciona o carro com sucesso")]
        [Trait("Adicionar", nameof(CarrosService))]
        public async Task DadoCarroNaoExistente_QuandoAdicionarChamado_RetornarMensagemSucesso()
        {
            //Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync((CarrosModel?)null);

            // Act
            var resultado = await _carrosService.AdicionarAsync(carro);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.AdicionarAsync(carro), Times.Once());
        }

        [Fact(DisplayName = "Lança uma exceção ao tentar adicionar um carro já existente")]
        [Trait("Adicionar", nameof(CarrosService))]
        public async Task DadoCarroExistente_QuandoAdicionarChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync(carro);

            // Act e Assert 
            await Assert.ThrowsAsync<Exception>(() => _carrosService.AdicionarAsync(carro));

            _carrosRepositoryMock.Verify(x => x.AdicionarAsync(It.IsAny<CarrosModel>()), Times.Never());
        }

        [Fact(DisplayName = "Atualiza os dados do carro com sucesso")]
        [Trait("Atualizar", nameof(CarrosService))]
        public async Task DadoIdExistente_QuandoAtualizarChamado_RetornarMensagemSucesso()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.AtualizarAsync(carro, id);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.AtualizarAsync(carro), Times.Once());
        }

        [Fact(DisplayName = "Lança uma exceção quando o Id informado estiver diferente")]
        [Trait("Atualizar", nameof(CarrosService))]
        public async Task DadoIdIncorreto_QuandoAtualizar_EntaoDeveLancarExcecaoENaoChamarAtualizar()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            int idDiferente = carro.Id + 1;

            // Act
            var excecaoLancada = await Record.ExceptionAsync(() => _carrosService.AtualizarAsync(carro, idDiferente));

            // Assert
            Assert.NotNull(excecaoLancada);
            Assert.IsType<Exception>(excecaoLancada);
            _carrosRepositoryMock.Verify(x => x.AtualizarAsync(It.IsAny<CarrosModel>()), Times.Never());
        }

        [Fact(DisplayName = "Lança uma exceção quando o Id não existir")]
        [Trait("Atualizar", nameof(CarrosService))]
        public async Task DadoIdNaoCadastrada_QuandoAtualizarChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.AtualizarAsync(carro, id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.AtualizarAsync(It.IsAny<CarrosModel>()), Times.Never());

        }


        [Fact(DisplayName = "Lança exceção ao tentar deletar um carro não exstente")]
        [Trait("Deletar", nameof(CarrosService))]
        public async Task DadoIdNaoEncontrado_QuandoDeletarForChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;  

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.DeletarAsync(id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.DeletarAsync(It.IsAny<int>()), Times.Never());
        }

        [Fact(DisplayName = "Deleta o carro com sucesso")]
        [Trait("Deletar", nameof(CarrosService))]
        public async Task DadoIdEncontrado_QuandoDeletarForChamado_Deletar()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.DeletarAsync(id);

            // Assert
            Assert.True(resultado);
            _carrosRepositoryMock.Verify(x => x.DeletarAsync(id), Times.Once());
        }


        [Fact(DisplayName = "Lança exceção quando o Id não for encontrado")]
        [Trait("Buscar por Id", nameof(CarrosService))]
        public async Task DadoIdNaoEncontrado_QuandoBuscarPorIdForChamado_RetornarMensagemExcecao()
        {
            // Arrange
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync((CarrosModel?)null);
            int id = carro.Id;

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.BuscarPorIdAsync(id));

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.BuscarPorIdAsync(It.IsAny<int>()), Times.Once());
        }
        [Fact(DisplayName = "Retorna o Carro por Id com sucesso")]
        [Trait("Buscar por Id", nameof(CarrosService))]
        public async Task DadoIdEncontrado_QuandoBuscarPorIdForChamado_RetornarCarroPorId()
        {
            // Arrange 
            var carro = _fixture.Create<CarrosModel>();
            _carrosRepositoryMock.Setup(x => x.BuscarPorIdAsync(carro.Id)).ReturnsAsync(carro);
            int id = carro.Id;

            // Act 
            var resultado = await _carrosService.BuscarPorIdAsync(id);

            // Assert
            Assert.NotNull(resultado);
            _carrosRepositoryMock.Verify(x => x.BuscarPorIdAsync(id), Times.Once());
        }


        [Fact(DisplayName = "Lança exceção ao não encontrar carros")]
        [Trait("Buscar Todos Carros", nameof(CarrosService))]
        public async Task DadoCarrosNaoCadastrados_QuandoBuscarTodosCarros_RetornarMensagemExcecao()
        {
            // Arrange 
            _carrosRepositoryMock.Setup(x => x.BuscarTodosCarrosAsync()).ReturnsAsync((List<CarrosModel>?)null);

            // Act
            var excecao = await Record.ExceptionAsync(() => _carrosService.BuscarTodosCarrosAsync());

            // Assert
            Assert.NotNull(excecao);
            Assert.IsType<Exception>(excecao);
            _carrosRepositoryMock.Verify(x => x.BuscarTodosCarrosAsync(), Times.Once());
        }



        [Fact(DisplayName = "Retorna todos os carros cadastrados com sucesso")]
        [Trait("Buscar Todos Carros", nameof(CarrosService))]
        public async Task DadoCarrosEncontrados_QuandoBuscarTodosCarrosForChamado_RetornarTodosCarros()
        {
            // Arrange
            var carros = _fixture.Create<List<CarrosModel>>();
            _carrosRepositoryMock.Setup(x => x.BuscarTodosCarrosAsync()).ReturnsAsync(carros);

            // Act
            var resultado = await _carrosService.BuscarTodosCarrosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(carros.Count, resultado.Count);
            _carrosRepositoryMock.Verify(x => x.BuscarTodosCarrosAsync(), Times.Once());
        }





    }

}







