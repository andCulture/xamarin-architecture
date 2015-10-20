using Core.Application.Interfaces.Settings;

namespace Infrastructure.Settings {
    public class ApplicationSettings : IApplicationSettings {
        public string APIEndpoint() {
            return "http://api.example.com/";
        }

        public string Locale() {
            return "en";
        }
    }
}
