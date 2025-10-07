using z=AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using Domain.Modules;
using Shared;

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

            CreateMap<Line_NameDto, Line_Name>();
            CreateMap<Line_Name, Line_NameDto>();

            CreateMap<Domain.Modules.Ticket_Prices, Shared.Ticket_PricesDto>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.StationsNumber, opt => opt.MapFrom(src => src.StationsNumber));

            CreateMap<Station_Coordinates, Station_CoordinatesDto>().ReverseMap();
            CreateMap<Rush_Times, Rush_TimeDto>().ReverseMap();

            CreateMap<Faults, FaultDto>().ReverseMap();
            CreateMap<Faults, FaultDto>()
    .ForMember(dest => dest.LineName, opt => opt.MapFrom(src => src.Line.LineName));

            CreateMap<CongestionSchedule, CongestionScheduleDto>()
    .ForMember(dest => dest.StationName, opt => opt.MapFrom(src => src.StationName.StationName))

    .ForMember(dest => dest.congestionLevel, opt => opt.MapFrom(src => src.CongestionLevel.ToString()))
    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
    .ReverseMap();

        }
    }
}
