using Android.App;
using Android.OS;
using Android.Support.V7.App;
using System.Threading;

namespace Presentation.Droid.Controllers {
    [Activity(Label = "Presetation.Droid", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true)]
    public class SplashActivity : AppCompatActivity {
        private InterfaceRegistrar registrar;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Register Interfaces
            registrar = new InterfaceRegistrar();

            // Pre-cache main objects
            registrar.WebRequestService.LoadAndCacheData();

            StartActivity(typeof(MainActivity));
        }
    }
}