using Core.Application.Injection;
using Core.Application.Interfaces.Services;

namespace Core.Application.Services {
    public abstract class ServiceBase {
        public ServiceBase() { }

        private ILoggingService _loggingService;
        protected ILoggingService LoggingService {
            get {
                if (_loggingService == null) {
                    _loggingService = Injector.Resolve<ILoggingService>();
                }
                return _loggingService;
            }
        }
    }
}
