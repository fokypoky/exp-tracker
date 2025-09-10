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
	public class AuthServiceRefreshTokenTests
	{
		private readonly Mock<IUsersService> _usersServiceMock;
		private readonly Mock<IAuthUtils> _authUtilsMock;
		private readonly IAuthService _authService;
		private readonly Fixture _fixture;

		public AuthServiceRefreshTokenTests()
		{
			_fixture = new Fixture();

			_usersServiceMock = new Mock<IUsersService>();
			_authUtilsMock = new Mock<IAuthUtils>();
			_authService = new Implementation.AuthService(_usersServiceMock.Object, _authUtilsMock.Object);
		}

		[Fact]
		public async Task RefreshToken_TokenExpired_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(true);

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task RefreshToken_TokenPayloadGuidNull_ShouldReturnNotAuthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);
			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(() => null);
			_authUtilsMock
				.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
				.Returns(() => null);

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task RefreshToken_TokenPayloadLoginNull_ShouldReturnNotAuthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);
			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(() => guid);
			_authUtilsMock
				.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
				.Returns(() => null);

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task RefreshToken_UserNotExists_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();
			var login = _fixture.Create<string>();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(() => guid);

			_authUtilsMock
				.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
				.Returns(() => login);

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>()
					{ Result = ResponseResult.NotFound, Data = null, Error = _fixture.Create<string>() });

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task RefreshToken_UserExistsAndRefreshTokenNotEquals_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();

			var guid = _fixture.Create<Guid>();
			var login = _fixture.Create<string>();
			var refreshToken = _fixture.Create<string>();
			var tokenPair = _fixture.Create<JwtTokenPair>();

			var user = _fixture.Build<User>()
				.With(_ => _.Id, guid)
				.With(_ => _.Login, login)
				.With(_ => _.RefreshToken, refreshToken)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(guid);

			_authUtilsMock
				.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
				.Returns(login);

			_authUtilsMock
				.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
				.Returns(tokenPair);

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);

			_usersServiceMock.Verify(_ => _.UpdateRefreshTokenAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public async Task RefreshToken_TokenValidAndUserExists_ShouldReturnCreated()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();

			var guid = _fixture.Create<Guid>();
			var login = _fixture.Create<string>();
			var tokenPair = _fixture.Create<JwtTokenPair>();

			var user = _fixture.Build<User>()
				.With(_ => _.Id, guid)
				.With(_ => _.Login, login)
				.With(_ => _.RefreshToken, request.RefreshToken)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(guid);

			_authUtilsMock
				.Setup(_ => _.GetLoginFromToken(request.RefreshToken))
				.Returns(login);

			_authUtilsMock
				.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
				.Returns(tokenPair);

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.RefreshToken(request);

			// Assert

			Assert.NotNull(result);
			Assert.Null(result.Error);
			Assert.NotNull(result.Data);
			Assert.Equal(ResponseResult.Ok, result.Result);

			_usersServiceMock.Verify(_ => _.UpdateRefreshTokenAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
		}
	}
}
