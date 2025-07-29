using System.Security.Claims;
using AutoFixture;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using ExpTracker.Entities.Dto.Responses.Auth;
using Moq;

namespace ExpTracker.Core.Tests.AuthService
{
	public class AuthServiceLogInTests
	{
		private readonly Mock<IUsersService> _usersServiceMock;
		private readonly Mock<IAuthUtils> _authUtilsMock;
		private readonly Fixture _fixture;
		private readonly IAuthService _authService;

		public AuthServiceLogInTests()
		{
			_fixture = new Fixture();

			_authUtilsMock = new Mock<IAuthUtils>();
			_usersServiceMock = new Mock<IUsersService>();
			_authService = new Implementation.AuthService(_usersServiceMock.Object, _authUtilsMock.Object);
		}

		[Fact]
		public async Task LogIn_UserExists_ShouldReturnOk()
		{
			// Arrange

			var request = _fixture.Create<AuthRequest>();
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

			_authUtilsMock
				.Setup(_ => _.HashPassword(request.Password))
				.Returns(passwordHash);
			_authUtilsMock
				.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
				.Returns(tokenPair);

			_usersServiceMock
				.Setup(_ => _.GetByLoginAndPasswordAsync(request.Login, passwordHash))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			_usersServiceMock
				.Setup(_ => _.UpdateRefreshTokenAsync(It.IsAny<User>(), It.IsAny<string>()))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.LogIn(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Data);
			Assert.Null(result.Error);

			_usersServiceMock.Verify(_ => _.UpdateRefreshTokenAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public async Task LogIn_UserNotFound_ShouldReturnNotFound()
		{
			// Arrange

			var request = _fixture.Create<AuthRequest>();
			var passwordHash = _fixture.Create<string>();
			var errorMessage = _fixture.Create<string>();

			_authUtilsMock
				.Setup(_ => _.HashPassword(request.Password))
				.Returns(passwordHash);

			_usersServiceMock
				.Setup(_ => _.GetByLoginAndPasswordAsync(request.Login, passwordHash))
				.ReturnsAsync(new ServiceResponse<User>()
					{ Result = ResponseResult.NotFound, Data = null, Error = errorMessage });

			// Act

			var result = await _authService.LogIn(request);

			// Assert

			Assert.NotNull(result);
			Assert.Null(result.Data);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.NotFound, result.Result);
			Assert.Equal(errorMessage, result.Error);
		}
	}
}
