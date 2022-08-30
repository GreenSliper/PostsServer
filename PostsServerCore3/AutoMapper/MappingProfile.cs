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
			CreateMap<AppUser, AppUserViewModel>()
				.ForMember(x => x.SubscribersCount, x => x.MapFrom<SubscribersResolver>())
				.ForMember(x => x.SubscribtionsCount, x => x.MapFrom<SubscriptionsResolver>());
			CreateMap<AppUserViewModel, AppUser>();
			CreateMap<Image, ImageVM>().ConvertUsing(typeof(ImageToVMConverter));
			CreateMap<ImageVM, Image>().ConvertUsing(typeof(VMToImageConverter));
		}
	}
}
