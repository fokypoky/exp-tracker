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
	public class AuthServiceRegisterTests
	{
		private readonly Mock<IUsersService> _usersServiceMock;
		private readonly Mock<IAuthUtils> _authUtilsMock;
		private readonly Fixture _fixture;
		private readonly IAuthService _authService;

		public AuthServiceRegisterTests()
		{
			_fixture = new Fixture();

			_usersServiceMock = new Mock<IUsersService>();
			_authUtilsMock = new Mock<IAuthUtils>();
			_authService = new Implementation.AuthService(_usersServiceMock.Object, _authUtilsMock.Object);
		}
		
		[Fact]
		public async Task Register_UserExists_ShouldReturnBadRequest()
		{
			// Arrange

			var authData = _fixture.Create<AuthRequest>();
			var user = _fixture.Build<User>()
				.With(_ => _.Login, authData.Login)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_usersServiceMock
				.Setup(_ => _.GetByLoginAsync(authData.Login))
				.ReturnsAsync(new ServiceResponse<User>() { Data = user, Result = ResponseResult.Ok });

			// Act

			var result = await _authService.Register(authData);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.NotNull(result.Error);
			Assert.Null(result.Data);
			Assert.Equal(ResponseResult.BadRequest, result.Result);
		}

		[Fact]
		public async Task Register_UserNotExists_ShouldReturnOk()
		{
			// Arrange

			var authData = _fixture.Create<AuthRequest>();
			var passwordHash = _fixture.Create<string>();
			var tokenPair = _fixture.Create<JwtTokenPair>();
			
			var user = _fixture.Build<User>()
				.With(_ => _.Login, authData.Login)
				.With(_ => _.Password, passwordHash)
				.Without(_ => _.Targets)
				.Without(_ => _.TransactionCategories)
				.Without(_ => _.Transactions)
				.Create();

			_authUtilsMock
				.Setup(_ => _.HashPassword(authData.Password))
				.Returns(passwordHash);
			_authUtilsMock
				.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
				.Returns(tokenPair);

			_usersServiceMock
				.Setup(_ => _.GetByLoginAsync(authData.Login))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.NotFound, Data = null, Error = null});

			_usersServiceMock
				.Setup(_ => _.CreateAsync(
					It.IsAny<Guid>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>()
				))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.Ok, Data = user, Error = null });

			// Act

			var result = await _authService.Register(authData);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Data);
			Assert.NotNull(result.Data.RefreshToken);
			Assert.NotNull(result.Data.AccessToken);
			Assert.Null(result.Error);
			Assert.Equal(ResponseResult.Ok, result.Result);
		}

		[Fact]
		public async Task Register_UserNotExistsAndSaveError_ShouldNotReturnOk()
		{
			// Arrange
			
			var request = _fixture.Create<AuthRequest>();
			var errorMessage = _fixture.Create<string>();
			var tokenPair = _fixture.Create<JwtTokenPair>();
			var passwordHash = _fixture.Create<string>();

			_authUtilsMock
				.Setup(_ => _.HashPassword(request.Password))
				.Returns(passwordHash);
			_authUtilsMock
				.Setup(_ => _.CreateJwtTokenPair(It.IsAny<List<Claim>>()))
				.Returns(tokenPair);

			_usersServiceMock
				.Setup(_ => _.GetByLoginAsync(request.Login))
				.ReturnsAsync(new ServiceResponse<User>() { Result = ResponseResult.NotFound, Data = null, Error = null });

			_usersServiceMock
				.Setup(_ => _.CreateAsync(
					It.IsAny<Guid>(),
					It.IsAny<string>(),
					It.IsAny<string>(),
					It.IsAny<string>()
				))
				.ReturnsAsync(new ServiceResponse<User>()
					{ Result = ResponseResult.InternalError, Data = null, Error = errorMessage });

			// Act

			var result = await _authService.Register(request);

			// Assert

			Assert.NotNull(result);
			Assert.NotNull(result.Error);
			Assert.Equal(ResponseResult.InternalError, result.Result);
			Assert.Equal(errorMessage, result.Error);
		}
	}
}
