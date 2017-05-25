using Autofac;
using AutoMapper;
using Mobile.Conductors;
using Mobile.Core.Interfaces.Conductors;
using Mobile.Core.Interfaces.DataAccess;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.DataAccess;
using Mobile.Presentation.Shared.MappingProfiles;
using Mobile.Services.Realm;
using Realms;
using System.Linq;
using System;

namespace Mobile.Presentation.Shared.ExtensionMethods
{
	public static class ContainerBuilderExtensions
	{
		#region Extension Methods

		/// <summary>
		/// Registers conductors.
		/// </summary>
		public static void RegisterConductors(this ContainerBuilder builder)
		{
			builder.RegisterType<LoginConductor<User>>().As<ILoginConductor<User>>();
		}

		/// <summary>
		/// Registers data access.
		/// </summary>
		public static void RegisterDataAccess(this ContainerBuilder builder)
		{
			//builder.RegisterType<Mapper>().As<IMapper>();
			builder.RegisterType<UserAccess<User>>().As<IUserDataAccess<User>>();
		}

		public static void RegisterMappings(this ContainerBuilder builder)
		{
			// Get the assembly for one of the custom mapping profile classes, 
			// and use it to get all custom profile types.
			var profiles =
			from t in typeof(UserMapProfile).Assembly.GetTypes()
			where typeof(Profile).IsAssignableFrom(t)
			select (Profile)Activator.CreateInstance(t);
			// Register each of the custom profiles.
			builder.Register(ctx => new MapperConfiguration(cfg =>
			{
				foreach (var profile in profiles)
				{
					cfg.AddProfile(profile);
				}
			}));
			// Register IMapper.
        	builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
		}

		/// <summary>
		/// Registers services.
		/// </summary>
		public static void RegisterServices(this ContainerBuilder builder)
		{
            // TODO: Change to a name that is representative of your app. 
            var dbName = "app.realm";
			// Get the context to Realm that the realm service layer requires.
			var realmContext = Realm.GetInstance(new RealmConfiguration(dbName)
			{
				SchemaVersion = 1
			});
			builder.Register(c => new UserService(realmContext)).As<IUserService<User>>();
		}

		#endregion Extension Methods
	}
}
