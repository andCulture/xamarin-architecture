
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobile.Svg;
using MvvmCross.Droid.Views;
using XamSvg;

namespace Mobile.Android.Views
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class LoginView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			// Setup SVG Lib
			XamSvg.Setup.InitSvgLib();
			// Config XamSvg which assembly to search for svg when "res:" is used
			XamSvg.Shared.Config.ResourceAssembly = typeof(SVGImages).GetTypeInfo().Assembly;
            UserDialogs.Init(this);
            base.OnCreate(bundle);
        }

		protected override void OnViewModelSet()
		{
			SetContentView(Resource.Layout.LoginLayout);
		}
    }
}
