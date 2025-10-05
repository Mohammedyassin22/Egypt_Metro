using z=AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace Services.Profile
{
    public class ProfileStationName : z.Profile
    {
        public ProfileStationName()
        {
            CreateMap<Domain.Modules.Station_Name, Shared.Station_NameDto>()
                .ForMember(dest => dest.StationName, opt => opt.MapFrom(src => src.StationName))
                .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => src.Coordinates))
                .ForMember(dest => dest.StationsLines, opt => opt.MapFrom(src => src.StationsLines));

            CreateMap<Domain.Modules.Line_Name,Shared.Line_NameDto>()
                 .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.LineName))
                 .ForMember(dest => dest.ColorCode, opt => opt.MapFrom(src => src.ColorCode));

            CreateMap<Domain.Modules.Ticket_Prices, Shared.Ticket_PricesDto>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.StationsNumber, opt => opt.MapFrom(src => src.StationsNumber));
        }
    }
}
