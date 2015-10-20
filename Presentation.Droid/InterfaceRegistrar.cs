using Core.Application.Injection;
using Core.Application.Interfaces.Services;
using InfrastructureApplicationRegistrar = Infrastructure.IoC.InterfaceRegistrar;

namespace Presentation.Droid {
    public class InterfaceRegistrar {
        private IConfigService _configService;
        public IConfigService ConfigService {
            get {
                if (_configService == null) {
                    _configService = Injector.Resolve<IConfigService>();
                }
                return _configService;
            }
        }

        private IUserService _userService;
        public IUserService UserService {
            get {
                if (_userService == null) {
                    _userService = Injector.Resolve<IUserService>();
                }
                return _userService;
            }
        }

        private IWebRequestService _webRequestService;
        public IWebRequestService WebRequestService {
            get {
                if (_webRequestService == null) {
                    _webRequestService = Injector.Resolve<IWebRequestService>();
                }
                return _webRequestService;
            }
        }

        public InterfaceRegistrar() {
            Load();
        }

        private void Load() {
            InfrastructureApplicationRegistrar.Load();
        }
    }
}