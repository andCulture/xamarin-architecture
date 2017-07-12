using Acr.UserDialogs;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Mobile.Services.Realm;
using AutoMapper;
using Mobile.Core.Interfaces.Services.Database;
using System.Reflection;

namespace Mobile.ViewModels
{
	public class App : MvvmCross.Core.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
            // Initialize mappings
            InitializeMappings();
			// Register AutoMapper
			Mvx.RegisterSingleton<IMapper>(() => Mapper.Instance);

            // Register Realm Services
            var assembly = typeof(Mobile.Services.Realm.Services.RepositoryService).GetTypeInfo().Assembly;
			assembly.CreatableTypes()
				.InNamespace("Mobile.Services.Realm")
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsSingleton();

			// Register Dialogs
			Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

			// Register  
			RegisterAppStart<ViewModels.LoginViewModel>();
		}

		private void InitializeMappings()
		{
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, Core.Models.User>();
				cfg.CreateMap<User, Core.Models.User>().ReverseMap(); 
            });
		}
	}
}
