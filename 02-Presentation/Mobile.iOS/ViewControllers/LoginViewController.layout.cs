using System;
using UIKit;

namespace Mobile.iOS.ViewControllers
{
    public partial class LoginViewController
    {
		#region Constants 

		private const float FIELD_SIZE = 30f;
		private const float MARGIN = 20f;

		#endregion Constants

		#region Private Methods

		/// <summary> 
		/// Lays out the subviews inside the presenter's super view. 
		/// </summary> 
		void ConfigureLayoutConstraints()
		{
			// Logo 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 2f * MARGIN),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AvatarImage.FillHeight),
				NSLayoutConstraint.Create(AvatarImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, AvatarImage.FillWidth)
		    });
			// Username 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, AvatarImage, NSLayoutAttribute.Bottom, 1, 2f * MARGIN),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(UsernameField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
	        });
			// Password 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, UsernameField, NSLayoutAttribute.Bottom, 1, MARGIN),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(PasswordField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
	        });
			// Login Button 
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, PasswordField, NSLayoutAttribute.Bottom, 1, MARGIN * 2f),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE * 1.5f),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(SubmitButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
	        });
		}

		#endregion Private Methods
	}
}
