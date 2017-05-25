using Autofac;
using AutoMapper;
using Mobile.Presentation.Shared.ExtensionMethods;
using Mobile.Presentation.Shared.MappingProfiles;

namespace Mobile.Presentation.Shared.ApplicationObjects
{
	public class AppSetup
	{
		#region Public Methods

		/// <summary>
		/// Creates the Autofac DI container, and registers classes.
		/// </summary>
		/// <returns>The DI container.</returns>
		public IContainer CreateContainer()
		{
			var containerBuilder = new ContainerBuilder();
			// Register Services, Data Access and Conductors
			containerBuilder.RegisterMappings();
			containerBuilder.RegisterServices();
			containerBuilder.RegisterDataAccess();
			containerBuilder.RegisterConductors();
			// Return built container
			return containerBuilder.Build();
		}

		public void InitObjectMapping()
		{
			// Wire-up object mapping
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile<UserMapProfile>();
			});
		}

		#endregion Public Methods
	}
}
