using AutoFixture;
using ExpTracker.Core.Categories.Commands.Update;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using Moq;

namespace ExpTracker.Core.Tests.Categories.Commands
{
    public class CategoriesUpdateHandlerTests
    {
        private readonly Mock<ICategoriesRepository> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly UpdateCategoryHandler _handler;

        public CategoriesUpdateHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<ICategoriesRepository>();
            _handler = new UpdateCategoryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Update_CategoryNotExists_ShouldReturnNotFound()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var request = _fixture.Create<TransactionCategoryDto>();

            var command = _fixture.Build<UpdateCategoryCommand>()
                .With(_ => _.Request, request)
                .With(_ => _.UserId, userId)
                .Create();

            _repositoryMock.Setup(_ => _.IsExistsAsync(request.Id, userId))
                .ReturnsAsync(false);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);

            Assert.Equal(ResponseResult.NotFound, result.Result);

            _repositoryMock.Verify(_ => _.IsExistsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<TransactionCategory>()), Times.Never);
        }

        [Fact]
        public async Task Update_CategoryExists_ShouldReturnOk()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var request = _fixture.Create<TransactionCategoryDto>();

            var command = _fixture.Build<UpdateCategoryCommand>()
                .With(_ => _.Request, request)
                .With(_ => _.UserId, userId)
                .Create();

            var category = _fixture.Build<TransactionCategory>()
                .With(_ => _.Id, request.Id)
                .With(_ => _.UserId, userId)
                .With(_ => _.Name, request.Name)
                .With(_ => _.Description, request.Description)
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .Create();

            _repositoryMock.Setup(_ => _.IsExistsAsync(request.Id, userId))
                .ReturnsAsync(true);
            _repositoryMock.Setup(_ => _.UpdateAsync(It.IsAny<TransactionCategory>()))
                .ReturnsAsync(category);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);

            Assert.Equal(ResponseResult.Ok, result.Result);
            Assert.Equal(request.Id, result.Data.Id);
            Assert.Equal(request.Description, result.Data.Description);
            Assert.Equal(request.Name, result.Data.Name);

            Assert.IsType<TransactionCategoryDto>(result.Data);

            _repositoryMock.Verify(_ => _.IsExistsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<TransactionCategory>()), Times.Once);
        }
    }
}
