using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Linq;
using Android.Content;
using Android.Runtime;

namespace Presentation.Droid.Controllers {
    [Activity(Label = "Presentation.Droid", Icon = "@drawable/icon")]
    public class MainActivity : BaseActivity {
        private TextView mViewHeaderText { get; set; }

        protected override void OnCreate(Bundle bundle) {
            OnCreate(bundle, Resource.Layout.Main, Resource.Menu.main_toolbar_actions, true);

            SupportActionBar.Title = "Home";

            // Add subpage view
            AddPageContent(Resource.Id.page_content, Resource.Layout.main_view);
        }
    }
}

