using Mobile.Core.Models.Views;
using Mobile.DataAccess;
using Mobile.Services.Realm;
using Shouldly;
using NUnit.Framework;
using AutoMapper;

namespace Mobile.Conductors.Tests
{
    [TestFixture ()]
	public class ProfileConductorTest : ConductorTestBase
	{
		#region Member Variables

		private ProfileConductor<User> _sut;
		private UserAccess<User> _userAccess;
		private UserService _userDatabaseService;

		#endregion Member Variables

		#region Setup

        [SetUp]
		public void ProfileConductorTestSetup()
		{
			_userDatabaseService = new UserService(realmContext);
			_userAccess = new UserAccess<User>(_userDatabaseService, Mapper.Instance);
			_sut = new ProfileConductor<User>(_userAccess);
		}

		#endregion Setup

		#region Get User Profile

		[Test()]
		public void Get_User_Profile_Fails_If_Id_Does_Not_Exist()
		{
			// Arrange
			UserView result = null;
			// Act
			result = _sut.GetUserProfile(9999);
			// Assert
			result.ShouldNotBeNull();
			result.HasErrors.ShouldBeTrue();
		}

		#endregion Get User Profile
	}
}
