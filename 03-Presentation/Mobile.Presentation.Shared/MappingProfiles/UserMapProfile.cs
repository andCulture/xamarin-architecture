using System;
using AutoMapper;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models.Views;

namespace Mobile.Presentation.Shared.MappingProfiles
{
	public class UserMapProfile : Profile
	{
		public UserMapProfile()
		{
			CreateMap<IUser, UserView>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
		}
	}
}
