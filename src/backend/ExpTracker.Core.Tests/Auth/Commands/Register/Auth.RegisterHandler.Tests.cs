using System.Security.Claims;
using AutoFixture;
using ExpTracker.Core.Auth.Commands.Register;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.DataAccess.PostgreSQL.Repositories.Interfaces;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using Moq;

namespace ExpTracker.Core.Tests.Auth.Commands.Register
{
    public class AuthRegisterHandlerTests
    {
        private Mock<IUsersRepository> _repositoryMock;
        private Mock<IAuthUtils> _utilsMock;
        private readonly Fixture _fixture;
        private readonly RegisterHandler _handler;

        public AuthRegisterHandlerTests()
        {
            _fixture = new Fixture();
            _repositoryMock = new Mock<IUsersRepository>();
            _utilsMock = new Mock<IAuthUtils>();
            _handler = new RegisterHandler(_repositoryMock.Object, _utilsMock.Object);
        }

        [Fact]
        public async Task Register_UserExists_ShouldReturnBadRequest()
        {
            // Arrange

            var request = _fixture.Create<AuthRequest>();
            var command = _fixture.Build<RegisterCommand>()
                .With(_ => _.Request, request)
                .Create();

            var user = _fixture.Build<User>()
                .With(_ => _.Login, request.Login)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _repositoryMock.Setup(_ => _.GetByLoginAsync(request.Login))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal(ResponseResult.BadRequest, result.Result);

            _repositoryMock.Verify(_ => _.GetByLoginAsync(It.IsAny<string>()), Times.Once);
            _repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Register_UserNotExists_ShouldReturnOk()
        {
            // Arrange

            var request = _fixture.Create<AuthRequest>();
            var command = _fixture.Build<RegisterCommand>()
                .With(_ => _.Request, request)
                .Create();

            var hash = _fixture.Create<string>();
            var guid = _fixture.Create<Guid>();
            var claims = new List<Claim>();
            var tokenPair = _fixture.Create<JwtTokenPair>();

            var user = _fixture.Build<User>()
                .With(_ => _.Id, guid)
                .With(_ => _.Login, request.Login)
                .With(_ => _.Password, hash)
                .Without(_ => _.Targets)
                .Without(_ => _.TransactionCategories)
                .Without(_ => _.Transactions)
                .Create();

            _utilsMock.Setup(_ => _.HashPassword(request.Password))
                .Returns(hash);
            _utilsMock.Setup(_ => _.CreateClaims(It.IsAny<string>(), request.Login))
                .Returns(claims);
            _utilsMock.Setup(_ => _.CreateJwtTokenPair(claims))
                .Returns(tokenPair);

            _repositoryMock.Setup(_ => _.GetByLoginAsync(request.Login))
                .ReturnsAsync(() => null);
            _repositoryMock.Setup(_ => _.CreateAsync(user))
                .ReturnsAsync(user);

            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.RefreshToken);
            Assert.NotNull(result.Data.AccessToken);
            Assert.Null(result.Error);
            Assert.Equal(ResponseResult.Ok, result.Result);

            Assert.IsType<JwtTokenPair>(result.Data);

            _repositoryMock.Verify(_ => _.GetByLoginAsync(It.IsAny<string>()), Times.Once);
            _repositoryMock.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
