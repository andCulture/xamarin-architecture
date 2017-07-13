using System;
using Autofac;

namespace Mobile.Presentation.Shared.ApplicationObjects
{
	/// <summary>
	/// Represents the shared Autofac DI registration container.
	/// </summary>
	public static class AppContainer
	{
		public static IContainer Container { get; set; }	
	}
}
