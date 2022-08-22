using AutoMapper;
using Domain.Models;
using PostsServerCore3.Models;
using System;

namespace PostsServerCore3.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AppUser, AppUserViewModel>().ReverseMap();
		}
	}
}
