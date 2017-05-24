using System.Collections.Generic;

namespace Mobile.Core.Models.Views
{
	public class BaseView
	{
		public List<Error> Errors { get; set; }

		public bool HasErrors
		{
			get
			{
				return Errors?.Count > 0;
			}
		}
	}
}
