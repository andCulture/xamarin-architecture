using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace Presentation.Droid.Handlers {
    public class ToolbarHandler {
        public ToolbarHandler(AppCompatActivity host, V7Toolbar toolbar, bool hasNavigation = true) {
            host.SetSupportActionBar(toolbar);

            host.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            if (hasNavigation) {
                host.SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_title_menu);
            }
        }
    }
}