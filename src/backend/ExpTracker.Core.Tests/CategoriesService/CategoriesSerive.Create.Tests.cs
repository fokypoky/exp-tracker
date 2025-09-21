using AutoFixture;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Categories;
using Moq;

namespace ExpTracker.Core.Tests.CategoriesService
{
    public class CategoriesServiceCreateTests
    {
        private readonly Mock<ICategoriesRepository> _catagoriesRepositoryMock;
        private readonly ICategoriesService _service;
        private readonly Fixture _fixture;

        public CategoriesServiceCreateTests()
        {
            _fixture = new Fixture();

            _catagoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _service = new Implementation.CategoriesService(_catagoriesRepositoryMock.Object);
        }

        [Fact]
        public async Task Create_CategoryExists_ShouldReturnBadRequest()
        {
            // Arrange
            var request = _fixture.Create<CreateCategoryRequest>();
            var userId = _fixture.Create<Guid>();
            var existingCategory = _fixture.Build<TransactionCategory>()
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .With(_ => _.Name, request.Name)
                .Create();

            _catagoriesRepositoryMock.Setup(_ => _.GetByNameAndUserIdAsync(request.Name, userId))
                .ReturnsAsync(existingCategory);

            // Act

            var result = await _service.CreateAsync(userId, request);

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(ResponseResult.BadRequest, result.Result);

            _catagoriesRepositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TransactionCategory>()), Times.Never);
        }

        [Fact]
        public async Task Create_CategoryNotExists_ShouldReturnOk()
        {
            // Arrange
            var request = _fixture.Create<CreateCategoryRequest>();
            var userId = _fixture.Create<Guid>();
            var createdCategory = _fixture.Build<TransactionCategory>()
                .With(_ => _.Name, request.Name)
                .With(_ => _.UserId, userId)
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .Create();

            _catagoriesRepositoryMock.Setup(_ => _.GetByNameAndUserIdAsync(request.Name, userId))
                .ReturnsAsync((TransactionCategory)null);

            _catagoriesRepositoryMock.Setup(_ => _.CreateAsync(It.IsAny<TransactionCategory>()))
                .ReturnsAsync(createdCategory);

            // Act
            var result = await _service.CreateAsync(userId, request);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
            Assert.Equal(ResponseResult.Ok, result.Result);
            Assert.Equal(result.Data.Name, createdCategory.Name);

            _catagoriesRepositoryMock.Verify(_ => _.CreateAsync(It.IsAny<TransactionCategory>()), Times.Once);
        }
    }
}
