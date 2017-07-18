using System.Threading.Tasks;
using Mobile.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Mobile.ViewModels.ViewModels
{
    public class HomeViewModel : BaseViewModel, IMvxViewModel<User>
    {
        #region Member Variables

        User _user;

		#endregion Member Variables

		#region Constructors

		public HomeViewModel() : base()
        {
        }

		#endregion Constructors

		#region Overrides
		
        public Task Initialize(User parameter)
		{
            User = parameter;
			return base.Initialize();
		}

        #endregion Overrides

        #region Public Properties

		public string EmailLabelText => AppText.EMAIL;

        public string FirstNameLabelText => AppText.FIRST_NAME;

        public string LastNameLabelText => AppText.LAST_NAME;

		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				SetProperty(ref _user, value);
			}
		}

        public string TitleLabelText => AppText.LOGIN_SUCCESS;

		#endregion Public Properties

		#region Public Methods

		#endregion Public Methods

		#region Private Methods

        #endregion Private Methods
    }
}