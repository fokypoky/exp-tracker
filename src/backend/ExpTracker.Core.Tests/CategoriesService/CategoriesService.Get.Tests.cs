using AutoFixture;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Categories;
using Moq;

namespace ExpTracker.Core.Tests.CategoriesService
{
    public class CategoriesServiceGetTests
    {
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;
        private readonly ICategoriesService _service;
        private readonly Fixture _fixture;

        public CategoriesServiceGetTests()
        {
            _fixture = new Fixture();

            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _service = new Implementation.CategoriesService(_categoriesRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_CollectionExists_ShouldReturnPartial()
        {
            // Arrange
            var request = _fixture.Build<GetCategoriesRequest>()
                .With(_ => _.Limit, 10)
                .With(_ => _.Offset, 0)
                .Create();

            var userId = _fixture.Create<Guid>();

            var collection = _fixture.Build<TransactionCategory>()
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .CreateMany(request.Limit)
                .ToList();

            var paginatedCollection = new PaginatedCollection<TransactionCategory>()
            {
                Data = collection,
                TotalCount = _fixture.Create<int>()
            };

            _categoriesRepositoryMock.Setup(_ => _.GetRangeAsync(userId, request.Limit, request.Offset))
                .ReturnsAsync(paginatedCollection);

            // Act

            var result = await _service.GetAsync(userId, request);

            // Assert
            Assert.NotNull(result);

            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Partial, result.Result);

            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Data);
            Assert.NotEmpty(result.Data.Data);

            Assert.Equal(result.Data.Data.Count, request.Limit);
        }

        [Fact]
        public async Task Get_CollectionNotExists_ShouldReturnPartialWithEmptyList()
        {
            // Arrange
            var request = _fixture.Build<GetCategoriesRequest>()
                .With(_ => _.Limit, 10)
                .With(_ => _.Offset, 0)
                .Create();

            var userId = _fixture.Create<Guid>();

            _categoriesRepositoryMock.Setup(_ => _.GetRangeAsync(userId, request.Limit, request.Offset))
                .ReturnsAsync(new PaginatedCollection<TransactionCategory>() { Data = new(), TotalCount = 0 });

            // Act

            var result = await _service.GetAsync(userId, request);

            // Assert
            Assert.NotNull(result);

            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Partial, result.Result);

            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Data);
            Assert.Empty(result.Data.Data);

            Assert.Equal(0, result.Data.TotalCount);
        }
    }
}
