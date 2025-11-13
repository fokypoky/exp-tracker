using AutoFixture;
using ExpTracker.Core.Categories.Commands.Create;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Categories;
using ExpTracker.Entities.Dto.Responses.Categories;
using Moq;

namespace ExpTracker.Core.Tests.Categories.Commands
{
    public class CategoriesCreateCategoryHandlerTests
    {
        private readonly Mock<ICategoriesRepository> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly CreateCategoryHandler _handler;

        public CategoriesCreateCategoryHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<ICategoriesRepository>();
            _handler = new CreateCategoryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Create_CategoryExists_ShouldReturnBadRequest()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();

            var request = _fixture.Create<CreateCategoryRequest>();
            var command = _fixture.Build<CreateCategoryCommand>()
                .With(_ => _.Request, request)
                .With(_ => _.UserId, userId)
                .Create();

            var existingCategory = _fixture.Build<TransactionCategory>()
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .With(_ => _.Name, request.Name)
                .Create();

            _repositoryMock.Setup(_ => _.GetByNameAndUserIdAsync(request.Name, command.UserId))
                .ReturnsAsync(existingCategory);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.BadRequest, result.Result);

            _repositoryMock.Verify(_ => _.GetByNameAndUserIdAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TransactionCategory>()), Times.Never);
        }

        [Fact]
        public async Task Create_CategoryNotExists_ShouldReturnOk()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();

            var request = _fixture.Create<CreateCategoryRequest>();
            var command = _fixture.Build<CreateCategoryCommand>()
                .With(_ => _.UserId, userId)
                .With(_ => _.Request, request)
                .Create();

            var category = _fixture.Build<TransactionCategory>()
                .With(_ => _.Name, request.Name)
                .With(_ => _.UserId, userId)
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .Create();

            _repositoryMock.Setup(_ => _.GetByNameAndUserIdAsync(request.Name, userId))
                .ReturnsAsync(() => null);

            _repositoryMock.Setup(_ => _.CreateAsync(It.IsAny<TransactionCategory>()))
                .ReturnsAsync(category);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Ok, result.Result);
            Assert.Equal(result.Data.Name, category.Name);

            Assert.IsType<CreateCategoryResponse>(result.Data);

            _repositoryMock.Verify(_ => _.GetByNameAndUserIdAsync(It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TransactionCategory>()), Times.Once);
        }
    }
}
