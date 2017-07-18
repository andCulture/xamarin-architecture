using System.Collections.Generic;
using Mobile.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Mobile.ViewModels.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public BaseViewModel()
        {
            Errors = new List<Error>();    
        }

		#region Public Properties

		public List<Error> Errors { get; set; }

		public bool HasErrors
		{
			get
			{
				return Errors?.Count > 0;
			}
		}

		#endregion Public Properties
    }
}
