using System.Security.Claims;
using AutoFixture;
using ExpTracker.Core.Auth.Commands.LogIn;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using Moq;

namespace ExpTracker.Core.Tests.Auth.Commands.LogIn
{
    public class AuthLogInHandlerTests
    {
        private readonly Mock<IUsersRepository> _repositoryMock;
        private readonly Mock<IAuthUtils> _utilsMock;
        private readonly Fixture _fixture;
        private readonly LogInHandler _handler;

        public AuthLogInHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IUsersRepository>();
            _utilsMock = new Mock<IAuthUtils>();
            _handler = new LogInHandler(_repositoryMock.Object, _utilsMock.Object);
        }

        [Fact]
        public async Task LogIn_UserExists_ShouldReturnOk()
        {
            // Arrange

            var request = _fixture.Create<AuthRequest>();
            var command = _fixture.Build<LogInCommand>()
                .With(_ => _.Request, request)
                .Create();

            var passwordHash = _fixture.Create<string>();
            var tokenPair = _fixture.Create<JwtTokenPair>();

            var user = _fixture.Build<User>()
                .With(_ => _.Login, request.Login)
                .With(_ => _.Password, passwordHash)
                .Without(_ => _.RefreshToken)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.HashPassword(request.Password))
                .Returns(passwordHash);
            _utilsMock.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
                .Returns(tokenPair);

            _repositoryMock.Setup(_ => _.GetByLoginAndPasswordAsync(request.Login, passwordHash))
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

            _repositoryMock.Verify(_ => _.GetByLoginAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LogIn_UserNotExists_ShouldReturnNotFound()
        {
            // Arrange

            var request = _fixture.Create<AuthRequest>();
            var command = _fixture.Build<LogInCommand>()
                .With(_ => _.Request, request)
                .Create();

            var passwordHash = _fixture.Create<string>();

            _utilsMock.Setup(_ => _.HashPassword(request.Password))
                .Returns(passwordHash);
            _repositoryMock.Setup(_ => _.GetByLoginAndPasswordAsync(request.Login, passwordHash))
                .ReturnsAsync(() => null);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.NotFound, result.Result);

            _repositoryMock.Verify(_ => _.GetByLoginAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _repositoryMock.Verify(_ => _.UpdateAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
