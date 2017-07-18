using UIKit;

namespace Mobile.iOS.ViewControllers
{
    public partial class HomeViewController
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
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Top, 1, 3f * MARGIN),
				NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, -MARGIN),
				NSLayoutConstraint.Create(TitleLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE)
		    });
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(FirstNameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, TitleLabel, NSLayoutAttribute.Bottom, 1, MARGIN),
				NSLayoutConstraint.Create(FirstNameLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(FirstNameLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(FirstNameLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
	        });
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(FirstNameValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, FirstNameLabel, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(FirstNameValue, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(FirstNameValue, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, MARGIN),
				NSLayoutConstraint.Create(FirstNameValue, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
			});
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(LastNameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, FirstNameLabel, NSLayoutAttribute.Bottom, 1, MARGIN),
				NSLayoutConstraint.Create(LastNameLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(LastNameLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(LastNameLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
			});
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(LastNameValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, LastNameLabel, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(LastNameValue, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(LastNameValue, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, MARGIN),
				NSLayoutConstraint.Create(LastNameValue, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
			});
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, LastNameLabel, NSLayoutAttribute.Bottom, 1, MARGIN),
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(EmailLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
			});
			ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(EmailValue, NSLayoutAttribute.Top, NSLayoutRelation.Equal, EmailLabel, NSLayoutAttribute.Top, 1, 0),
				NSLayoutConstraint.Create(EmailValue, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(EmailValue, NSLayoutAttribute.Right, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Right, 1, MARGIN),
				NSLayoutConstraint.Create(EmailValue, NSLayoutAttribute.Width, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.Width, .5f, -MARGIN)
			});
		}

		#endregion Private Methods
	}
}
