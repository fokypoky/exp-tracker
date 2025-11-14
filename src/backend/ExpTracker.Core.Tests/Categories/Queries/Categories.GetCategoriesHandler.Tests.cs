using AutoFixture;
using ExpTracker.Core.Categories.Queries.GetCategories;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using ExpTracker.Entities.Dto.Requests.Categories;
using Moq;

namespace ExpTracker.Core.Tests.Categories.Queries
{
    public class CategoriesGetCategoriesHandlerTests
    {
        private readonly Mock<ICategoriesRepository> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly GetCategoriesHandler _handler;

        public CategoriesGetCategoriesHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<ICategoriesRepository>();
            _handler = new GetCategoriesHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Get_CollectionExists_ShouldReturnPartial()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();

            var request = _fixture.Build<GetCategoriesRequest>()
                .With(_ => _.Limit, 10)
                .With(_ => _.Offset, 0)
                .Create();
            var query = _fixture.Build<GetCategoriesQuery>()
                .With(_ => _.UserId, userId)
                .With(_ => _.Request, request)
                .Create();

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

            _repositoryMock.Setup(_ => _.GetRangeAsync(userId, request.Limit, request.Offset))
                .ReturnsAsync(paginatedCollection);

            // Act

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Error);

            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Data);
            Assert.NotEmpty(result.Data.Data);

            Assert.IsType<PaginatedResponse<TransactionCategoryDto>>(result.Data);
            Assert.IsType<List<TransactionCategoryDto>>(result.Data.Data);

            Assert.Equal(ResponseResult.Partial, result.Result);

            _repositoryMock.Verify(_ => _.GetRangeAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_CollectionNotExists_ShouldReturnPartialWithEmptyList()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var request = _fixture.Build<GetCategoriesRequest>()
                .With(_ => _.Limit, 10)
                .With(_ => _.Offset, 0)
                .Create();

            var query = _fixture.Build<GetCategoriesQuery>()
                .With(_ => _.UserId, userId)
                .With(_ => _.Request, request)
                .Create();

            _repositoryMock.Setup(_ => _.GetRangeAsync(userId, request.Limit, request.Offset))
                .ReturnsAsync(new PaginatedCollection<TransactionCategory>() { Data = [], TotalCount = 0 });

            // Act

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Error);

            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Data);
            Assert.Empty(result.Data.Data);

            Assert.IsType<PaginatedResponse<TransactionCategoryDto>>(result.Data);
            Assert.IsType<List<TransactionCategoryDto>>(result.Data.Data);

            Assert.Equal(ResponseResult.Partial, result.Result);
            Assert.Equal(0, result.Data.TotalCount);

            _repositoryMock.Verify(_ => _.GetRangeAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
