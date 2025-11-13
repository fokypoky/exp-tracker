using AutoFixture;
using ExpTracker.Core.Auth.Commands.LogOut;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using Moq;

namespace ExpTracker.Core.Tests.Auth.Commands.LogOut
{
    public class AuthLogOutHandlerTests
    {
        private readonly Mock<IUsersRepository> _repositoryMock;
        private readonly Mock<IAuthUtils> _utilsMock;
        private readonly Fixture _fixture;
        private readonly LogOutHandler _handler;

        public AuthLogOutHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IUsersRepository>();
            _utilsMock = new Mock<IAuthUtils>();
            _handler = new LogOutHandler(_repositoryMock.Object, _utilsMock.Object);
        }

        [Fact]
        public async Task LogOut_InvalidRefreshTokenPayload_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<LogOutCommand>()
                .With(_ => _.Request, request)
                .Create();

            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(() => null);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.Unauthorized, result.Result);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Never);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LogOut_UserNotExists_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<LogOutCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();

            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _repositoryMock.Setup(_ => _.GetAsync(guid))
                .ReturnsAsync(() => null);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.Unauthorized, result.Result);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LogOut_UserExistsAndRefreshTokenNotEquals_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<LogOutCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();
            var actualRefreshToken = _fixture.Create<string>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.RefreshToken, actualRefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);

            _repositoryMock.Setup(_ => _.GetAsync(guid))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.Unauthorized, result.Result);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LogOut_UserExistsAndRefreshTokenExpired_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<LogOutCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.RefreshToken, request.RefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(true);

            _repositoryMock.Setup(_ => _.GetAsync(guid))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.Unauthorized, result.Result);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task LogOut_UserExistsAndRefreshTokenValid_ShouldReturnCreated()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<LogOutCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.RefreshToken, request.RefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);

            _repositoryMock.Setup(_ => _.GetAsync(guid))
                .ReturnsAsync(user);
            _repositoryMock.Setup(_ => _.UpdateAsync(user))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Created, result.Result);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
