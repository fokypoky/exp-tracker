using AutoFixture;
using ExpTracker.Core.Categories.Commands.Delete;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using Moq;

namespace ExpTracker.Core.Tests.Categories.Commands
{
    public class Categories
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICategoriesRepository> _repositoryMock;
        private readonly DeleteCategoryHandler _handler;

        public Categories()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<ICategoriesRepository>();
            _handler = new DeleteCategoryHandler(_repositoryMock.Object);
        }


        [Fact]
        public async Task Delete_CategoryNotExists_ShouldReturnNotFound()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var categoryId = _fixture.Create<Guid>();

            var command = new DeleteCategoryCommand(categoryId, userId);

            _repositoryMock.Setup(_ => _.GetByIdAndUserIdAsync(categoryId, userId))
                .ReturnsAsync(() => null);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);

            Assert.Equal(ResponseResult.NotFound, result.Result);

            _repositoryMock.Verify(_ => _.GetByIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.IsRelatedAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(_ => _.DeleteAsync(It.IsAny<TransactionCategory>()), Times.Never);
        }

        [Fact]
        public async Task Delete_CategoryIsRelated_ShouldReturnBadRequest()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var categoryId = _fixture.Create<Guid>();

            var category = _fixture.Build<TransactionCategory>()
                .With(e => e.UserId, userId)
                .With(e => e.Id, categoryId)
                .Without(e => e.Transactions)
                .Without(e => e.User)
                .Create();

            var command = new DeleteCategoryCommand(categoryId, userId);

            _repositoryMock.Setup(_ => _.GetByIdAndUserIdAsync(categoryId, userId))
                .ReturnsAsync(category);
            _repositoryMock.Setup(_ => _.IsRelatedAsync(categoryId))
                .ReturnsAsync(true);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);

            Assert.Equal(ResponseResult.BadRequest, result.Result);

            _repositoryMock.Verify(_ => _.GetByIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.IsRelatedAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.DeleteAsync(It.IsAny<TransactionCategory>()), Times.Never);
        }

        [Fact]
        public async Task Delete_CategoryExistsAndNotRelated_ShouldReturnCreated()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var categoryId = _fixture.Create<Guid>();

            var category = _fixture.Build<TransactionCategory>()
                .With(e => e.UserId, userId)
                .With(e => e.Id, categoryId)
                .Without(e => e.Transactions)
                .Without(e => e.User)
                .Create();

            var command = new DeleteCategoryCommand(categoryId, userId);

            _repositoryMock.Setup(_ => _.GetByIdAndUserIdAsync(categoryId, userId))
                .ReturnsAsync(category);
            _repositoryMock.Setup(_ => _.IsRelatedAsync(categoryId))
                .ReturnsAsync(false);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);

            Assert.Equal(ResponseResult.Created, result.Result);

            _repositoryMock.Verify(_ => _.GetByIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.IsRelatedAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.DeleteAsync(It.IsAny<TransactionCategory>()), Times.Once);
        }
    }
}
