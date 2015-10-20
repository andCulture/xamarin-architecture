using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Presentation.Droid.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Presentation.Droid.Controllers {
    public abstract class BaseActivity : AppCompatActivity {

        protected bool HasNavigation { get; set; }
        protected int ToolbarActionsId { get; set; }
        protected ToolbarHandler ToolbarHandler { get; set; }
        protected NavigationDrawerHandler NavigationDrawerHandler { get; set; }
        protected InterfaceRegistrar Registrar { get; set; }
        protected Intent mParentIntent { get; set; }
        protected Func<IMenuItem, bool> FavoriteFunction { get; set; }

        /// <summary>
        /// Expects delegate function to handle setting the favorite icon on toolbar
        /// </summary>
        protected Func<IMenu, bool> MenuPostCreateFunction { get; set; }

        protected void OnCreate(Bundle bundle, int viewId, int toolbarActionsId, bool hasNavigation, int selectedDrawerItemId = Resource.Id.nav_home) {
            base.OnCreate(bundle);

            SetContentView(viewId);
            HasNavigation = hasNavigation;
            ToolbarActionsId = toolbarActionsId;

            // Register Interfaces
            Registrar = new InterfaceRegistrar();

            ToolbarHandler = new ToolbarHandler(this, FindViewById<V7Toolbar>(Resource.Id.toolbar), HasNavigation);
            SupportActionBar.Title = string.Empty;

            NavigationDrawerHandler = new NavigationDrawerHandler(this,
                FindViewById<NavigationView>(Resource.Id.nav_view),
                FindViewById<DrawerLayout>(Resource.Id.drawer_layout),
                selectedDrawerItemId);
        }
        
        protected View AddPageContent(int parentViewId, int contentLayoutId) {
            var vi = (LayoutInflater)ApplicationContext.GetSystemService(LayoutInflaterService);
            var view = (ViewGroup)FindViewById(parentViewId);
            return vi.Inflate(contentLayoutId, view);
        }

        protected View InflatePageContent(int parentViewId, int contentLayoutId) {
            return InflatePageContent(FindViewById<ViewGroup>(parentViewId), contentLayoutId);
        }

        protected View InflatePageContent(ViewGroup view, int contentLayoutId) {
            var vi = (LayoutInflater)ApplicationContext.GetSystemService(LayoutInflaterService);
            return vi.Inflate(contentLayoutId, view, false);
        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(ToolbarActionsId, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:
                    if (HasNavigation) {
                        NavigationDrawerHandler.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    } else {
                        if (mParentIntent != null) {
                            SetResult(Result.Ok, mParentIntent);
                        }
                        Finish();
                    }
                    return true;
                case Resource.Id.action_favorite:
                    if (FavoriteFunction != null) {
                        return FavoriteFunction(item);
                    }
                    return true;
                default:
                    Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}