using System.Security.Claims;
using AutoFixture;
using ExpTracker.Core.Auth.Commands.Refresh;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using Moq;

namespace ExpTracker.Core.Tests.Auth.Commands.Refresh
{
    public class AuthRefreshTokenHandlerTests
    {
        private readonly Mock<IUsersRepository> _repositoryMock;
        private readonly Mock<IAuthUtils> _utilsMock;
        private readonly Fixture _fixture;
        private readonly RefreshTokenHandler _handler;

        public AuthRefreshTokenHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IUsersRepository>();
            _utilsMock = new Mock<IAuthUtils>();
            _handler = new RefreshTokenHandler(_repositoryMock.Object, _utilsMock.Object);
        }

        [Fact]
        public async Task RefreshToken_TokenExpired_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(true);

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
        public async Task RefreshToken_TokenPayloadGuidNull_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);
            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(() => null);
            _utilsMock.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
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
        public async Task RefreshToken_TokenPayloadLoginNull_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);
            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
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
        public async Task RefreshToken_TokenValidUserNotExists_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();
            var login = _fixture.Create<string>();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);
            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
                .Returns(login);

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
        public async Task RefreshToken_UserExistsAndRefreshTokensNotEqual_ShouldReturnUnauthorized()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();
            var login = _fixture.Create<string>();
            var actualRefreshToken = _fixture.Create<string>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.Login, login)
                .With(_ => _.RefreshToken, actualRefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);
            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
                .Returns(login);

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
        public async Task RefreshToken_TokenValidAndUserExists_ShouldReturnOk()
        {
            // Arrange

            var request = _fixture.Create<RefreshTokenRequest>();
            var command = _fixture.Build<RefreshTokenCommand>()
                .With(_ => _.Request, request)
                .Create();

            var guid = _fixture.Create<Guid>();
            var login = _fixture.Create<string>();

            var claims = new List<Claim>();
            var tokenPair = _fixture.Create<JwtTokenPair>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.Login, login)
                .With(_ => _.RefreshToken, request.RefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.IsTokenExpired(request.RefreshToken))
                .Returns(false);
            _utilsMock.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
                .Returns(guid);
            _utilsMock.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
                .Returns(login);
            _utilsMock.Setup(_ => _.CreateClaims(user.Id.ToString(), user.Login))
                .Returns(claims);
            _utilsMock.Setup(_ => _.CreateJwtTokenPair(claims))
                .Returns(tokenPair);

            _repositoryMock.Setup(_ => _.GetAsync(guid))
                .ReturnsAsync(user);
            _repositoryMock.Setup(_ => _.UpdateAsync(user))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Ok, result.Result);

            Assert.IsType<JwtTokenPair>(result.Data);

            _repositoryMock.Verify(_ => _.GetAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
