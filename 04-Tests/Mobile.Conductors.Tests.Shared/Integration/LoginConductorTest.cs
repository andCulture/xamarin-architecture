using Mobile.DataAccess;
using Mobile.Services.Realm;
using Shouldly;
using NUnit.Framework;
using AutoMapper;

namespace Mobile.Conductors.Tests
{
    [TestFixture ()]
	public class LoginConductorTest : ConductorTestBase
	{
		#region Member Variables

		private LoginConductor<User> _sut;
		private UserAccess<User> _userAccess;
		private UserService _userDatabaseService;

		#endregion Member Variables

		#region Setup

        [SetUp]
		public void LoginConductorTestSetup()
		{
			_userDatabaseService = new UserService(realmContext);
			_userAccess = new UserAccess<User>(_userDatabaseService, Mapper.Instance);
			_sut = new LoginConductor<User>(_userAccess);
		}

		#endregion Setup

		#region Login User

		[Test()]
		public void LoginUser_User_Does_Not_Exist_Has_Errors()
		{
			// Arrange
			// Act
			var result = _sut.LoginUser("test", "test");
			// Assert
			result.ShouldNotBeNull();
			result.HasErrors.ShouldBeTrue();
		}

		#endregion Login User
	}
}
