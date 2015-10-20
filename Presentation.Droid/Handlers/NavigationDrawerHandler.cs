using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Presentation.Droid.Controllers;

namespace Presentation.Droid.Handlers {
    public class NavigationDrawerHandler {

        private AppCompatActivity mHostActivity;
        private DrawerLayout mDrawerLayout;
        private NavigationView mNavView;
        private int mSelectedResource;

        public NavigationDrawerHandler(AppCompatActivity host, NavigationView navView, DrawerLayout drawerLayout, int? selectedResource) {
            mHostActivity = host;
            mDrawerLayout = drawerLayout;
            mNavView = navView;

            if (mNavView != null && mDrawerLayout != null) {
                setupDrawerContent(mNavView);
            }

            if (selectedResource.HasValue && selectedResource.Value > 0) {
                mSelectedResource = selectedResource.Value;
                mNavView.Menu.FindItem(selectedResource.Value).SetChecked(true);
            }
        }

        void setupDrawerContent(NavigationView navigationView) {
            navigationView.NavigationItemSelected += (sender, e) => {
                e.MenuItem.SetChecked(true);
                mDrawerLayout.CloseDrawers();

                if (mSelectedResource == e.MenuItem.ItemId) {
                    return;
                }

                switch (e.MenuItem.ItemId) {
                    case Resource.Id.nav_home:
                        mHostActivity.StartActivity(typeof(MainActivity));
                        break;
                    default:
                        Toast.MakeText(mHostActivity, "Menu Selected: " + e.MenuItem.TitleFormatted, ToastLength.Short).Show();
                        break;
                }
            };
        }

        public void OpenDrawer(int gravity) {
            mDrawerLayout.OpenDrawer(gravity);
        }
    }
}