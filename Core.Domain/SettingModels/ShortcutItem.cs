namespace Core.Domain.SettingModels {
    public class ShortcutItem {
        private string _controller;
        private string _image;

        public ShortcutItem(string controller, string image) {
            _controller = controller;
            _image = image;
        }

        public string Controller() {
            return _controller;
        }

        public string Image() {
            return _image;
        }
    }
}
