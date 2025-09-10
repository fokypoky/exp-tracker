using AutoFixture;
using ExpTracker.Core.Implementation;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using Moq;

namespace ExpTracker.Core.Tests.ProfileTests
{
    public class ProfileServiceGetTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly Fixture _fixture;
        private readonly IProfileService _profileService;

        public ProfileServiceGetTests()
        {
            _fixture = new Fixture();

            _usersRepositoryMock = new Mock<IUsersRepository>();
            _profileService = new ProfileService(_usersRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Get_UserNotExists_ShouldReturnNotFound()
        {
            // Arrange
            
            var login = _fixture.Create<string>();
            _usersRepositoryMock
                .Setup(_ => _.GetByLoginAsync(login))
                .ReturnsAsync((User)null);
            
            // Act

            var result = await _profileService.GetAsync(login);

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(ResponseResult.NotFound, result.Result);
        }

        [Fact]
        public async Task Get_UserExists_ShouldReturnUser()
        {
            // Arrange
            
            var user = _fixture.Build<User>()
                .Without(_ => _.Transactions)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Targets)
                .Create();

            _usersRepositoryMock
                .Setup(_ => _.GetByLoginAsync(user.Login))
                .ReturnsAsync(user);

            // Act

            var result = await _profileService.GetAsync(user.Login);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Ok, result.Result);
            
            Assert.Equal(user.Login, result.Data.Login);
            Assert.Equal(user.Id, result.Data.Guid);
            Assert.Equal(user.Registered, result.Data.Registered);
        }
    }
}