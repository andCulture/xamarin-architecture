using Core.Application.Injection;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using Core.Domain.Entities;
using Core.Domain.Interfaces.Repositories;
using Infrastructure.Settings;
using Infrastructure.SQLite.Repositories;

namespace Infrastructure.IoC {
    public class InterfaceRegistrar {
        public InterfaceRegistrar() { }

        public static void Load() {
            #region Repository Injection
            Injector.Register<IRepository<User>>(() => new SQLiteRepository<User>());
            #endregion

            #region Service Injection
            Injector.Register<IConfigService>(() => new ConfigService(new ApplicationSettings()));
            Injector.Register<ILoggingService>(() => new LoggingService());
            Injector.Register<IUserService>(() => new UserService());
            Injector.Register<IWebRequestService>(() => new WebRequestService());
            #endregion

            #region Broadcast Injection
            #endregion
        }
    }
}
