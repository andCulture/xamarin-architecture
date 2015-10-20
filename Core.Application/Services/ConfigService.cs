using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Settings;

namespace Core.Application.Services {
    public class ConfigService : ServiceBase, IConfigService {
        private IApplicationSettings _applicationSettings;

        public ConfigService(IApplicationSettings settings) {
            _applicationSettings = settings;
        }

        public string APIEndpoint() {
            if (_applicationSettings == null) {
                return null;
            }
            return _applicationSettings.APIEndpoint();
        }

        public string Locale() {
            if (_applicationSettings == null) {
                return "en";
            }
            return _applicationSettings.Locale();
        }
    }
}
