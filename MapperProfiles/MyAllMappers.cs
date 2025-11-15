using form_task_arda.Data;
using form_task_arda.DTOs;
using form_task_arda.Models;
using AutoMapper;

namespace form_task_arda.MapperProfiles
{
	// Create main mapper class, get inheritance from "Profile", profile is the special object of the Auto-Mapper library.
	public class MyAllMappers : Profile
	{
		public MyAllMappers()
		{
			CreateMap<GiderlerModel, GiderlerDTO>()
			.ForMember(dest => dest.Gider_Adi, opt => opt.MapFrom(e => e.Gider_Adi))
			.ForMember(dest => dest.Gider_Kategorisi, opt => opt.MapFrom(e => e.Gider_Kategorisi))
			.ForMember(dest => dest.Gider_Tedarikcisi, opt => opt.MapFrom(e => e.Gider_Tedarikcisi))
			.ForMember(dest => dest.Gider_Ucreti, opt => opt.MapFrom(e => e.Gider_Maliyeti))
			.ForMember(dest => dest.Gider_Tarihi, opt => opt.MapFrom(e => e.Gider_Tarihi));


			CreateMap<GiderlerDTO, GiderlerModel>()
				.ForMember(dest => dest.Gider_Maliyeti, opt => opt.MapFrom(e => e.Gider_Ucreti));
				//.ForMember(dest => dest.Gider_Tedarikcisi, opt => opt.Ignore());
				//.ForMember(dest => dest.Gider_Id, opt => opt.Ignore());
		}
	}
}
