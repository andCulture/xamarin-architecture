using Realms;

namespace Mobile.Conductors.Tests
{
	public class ConductorTestBase
	{
        #region Constants

        // TODO: Change this to a name that is representative of your app.
        private const string DATABASE_NAME = "app.realm";

		#endregion Constants
		
        #region Protected Variables

		protected readonly Realm realmContext;

		#endregion Protected Variables

		#region Constructor

		public ConductorTestBase()
		{
			realmContext = Realm.GetInstance(new RealmConfiguration(DATABASE_NAME) { 
				SchemaVersion = 1
			});
		}

		#endregion Constructor
	}
}
