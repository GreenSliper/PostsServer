using AutoMapper;
using Domain.Models;
using System;

namespace PostsServerCore3.Models
{
	public class ImageVM
	{
		public string? Id { get; set; }
		public string Base64Data { get; set; }
	}

	public class ImageToVMConverter : ITypeConverter<Image, ImageVM>
	{
		public ImageVM Convert(Image source, ImageVM destination, ResolutionContext context)
		{
			if(source == null)
				return null;
			return new ImageVM()
			{
				Id = source.Id,
				Base64Data = source.Bytes == null ? null : System.Convert.ToBase64String(source.Bytes)
			};
		}
	}

	public class VMToImageConverter : ITypeConverter<ImageVM, Image>
	{
		public Image Convert(ImageVM source, Image destination, ResolutionContext context)
		{
			if (source == null)
				return null;
			return new Image()
			{
				Id = source.Id,
				Bytes = source.Base64Data == null ? null : System.Convert.FromBase64String(source.Base64Data)
			};
		}
	}
}
