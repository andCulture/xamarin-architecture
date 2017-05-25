using System;
using UIKit;

namespace Mobile.iOS.ViewControllers.Login
{
	public class LoginPresenterSettings
	{
		internal UIView         ContentView             { get; set; }
        internal EventHandler   OnSubmitButtonTapped    { get; set; }
	}
}
