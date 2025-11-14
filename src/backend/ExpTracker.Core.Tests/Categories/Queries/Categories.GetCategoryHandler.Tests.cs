using AutoFixture;
using ExpTracker.Core.Categories.Queries.GetCategory;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto;
using Moq;

namespace ExpTracker.Core.Tests.Categories.Queries
{
    public class CategoriesGetCategoryHandlerTests
    {
        private readonly Mock<ICategoriesRepository> _repository;
        private readonly Fixture _fixture;
        private readonly GetCategoryHandler _handler;

        public CategoriesGetCategoryHandlerTests()
        {
            _fixture = new Fixture();
            _repository = new Mock<ICategoriesRepository>();
            _handler = new GetCategoryHandler(_repository.Object);
        }

        [Fact]
        public async Task Get_CategoryNotExists_ShouldReturnNotFound()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var request = _fixture.Create<Guid>();

            var query = _fixture.Build<GetCategoryQuery>()
                .With(_ => _.UserId, userId)
                .With(_ => _.CategoryId, request)
                .Create();
            
            _repository.Setup(_ => _.GetByIdAndUserIdAsync(request, userId))
                .ReturnsAsync(() => null);

            // Act

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            
            Assert.Equal(ResponseResult.NotFound, result.Result);
            
            _repository.Verify(_ => _.GetByIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Get_CategoryExists_ShouldReturnOk()
        {
            // Arrange

            var userId = _fixture.Create<Guid>();
            var request = _fixture.Create<Guid>();

            var query = _fixture.Build<GetCategoryQuery>()
                .With(_ => _.CategoryId, request)
                .With(_ => _.UserId, userId)
                .Create();
            
            var category = _fixture.Build<TransactionCategory>()
                .With(_ => _.Id, request)
                .With(_ => _.UserId, userId)
                .Without(_ => _.Transactions)
                .Without(_ => _.User)
                .Create();

            _repository.Setup(_ => _.GetByIdAndUserIdAsync(request, userId))
                .ReturnsAsync(category);

            // Act

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);

            Assert.IsType<ServiceResponse<TransactionCategoryDto>>(result);
            Assert.IsType<TransactionCategoryDto>(result.Data);
            
            Assert.Equal(ResponseResult.Ok, result.Result);
            
            _repository.Verify(_ => _.GetByIdAndUserIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}