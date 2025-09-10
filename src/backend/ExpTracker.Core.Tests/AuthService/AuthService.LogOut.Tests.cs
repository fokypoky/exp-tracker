using AutoFixture;
using ExpTracker.Core.Interfaces;
using ExpTracker.Core.Models;
using ExpTracker.Entities.Common;
using ExpTracker.Entities.Dto.Requests.Auth;
using Moq;

namespace ExpTracker.Core.Tests.AuthService
{
	public class AuthServiceLogOutTests
	{
		private readonly Mock<IUsersService> _usersServiceMock;
		private readonly Mock<IAuthUtils> _authUtilsMock;
		private readonly Fixture _fixture;
		private readonly IAuthService _authService;

		public AuthServiceLogOutTests()
		{
			_fixture = new Fixture();

			_usersServiceMock = new Mock<IUsersService>();
			_authUtilsMock = new Mock<IAuthUtils>();
			_authService = new Implementation.AuthService(_usersServiceMock.Object, _authUtilsMock.Object);
		}

		[Fact]
		public async Task LogOut_InvalidRefreshTokenPayload_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(() => null);

			// Act

			var result = await _authService.LogOut(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task LogOut_UserNotExists_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();
			var errorMessage = _fixture.Create<string>();

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(guid);
			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>()
					{ Result = ResponseResult.NotFound, Data = null, Error = errorMessage });

			// Act

			var result = await _authService.LogOut(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task LogOut_UserExistsAndRefreshTokenNotEquals_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();
			var refreshToken = _fixture.Create<string>();

			var user = _fixture.Build<User>()
				.With(_ => _.Id, guid)
				.With(_ => _.RefreshToken, refreshToken)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken));

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.LogOut(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);
		}

		[Fact]
		public async Task LogOut_UserExistsAndRefreshTokenExpired_ShouldReturnUnauthorized()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();

			var user = _fixture.Build<User>()
				.With(_ => _.Id, guid)
				.With(_ => _.RefreshToken, request.RefreshToken)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(guid);

			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(true);

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.LogOut(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.Unauthorized, result.Result);

			_usersServiceMock.Verify(_ => _.UpdateRefreshTokenAsync(user, It.IsAny<string>()), Times.Never);
		}

		[Fact]
		public async Task LogOut_UserExistsAndRefreshTokenValid_ShouldReturnOk()
		{
			// Arrange

			var request = _fixture.Create<RefreshTokenRequest>();
			var guid = _fixture.Create<Guid>();

			var user = _fixture.Build<User>()
				.With(_ => _.Id, guid)
				.With(_ => _.RefreshToken, request.RefreshToken)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.GetGuidFromToken(request.RefreshToken))
				.Returns(guid);
			_authUtilsMock
				.Setup(_ => _.IsTokenExpired(request.RefreshToken))
				.Returns(false);

			_usersServiceMock
				.Setup(_ => _.GetByIdAsync(guid))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.LogOut(request);

			// Assert

			Assert.NotNull(result);
			Assert.Null(result.Error);
			Assert.Equal(ResponseResult.Created, result.Result);

			_usersServiceMock.Verify(_ => _.UpdateRefreshTokenAsync(user, It.IsAny<string>()), Times.Once);
		}
	}
}
