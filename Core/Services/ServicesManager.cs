
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Services.Hubs;
using ServicesAbstraction;
using ServicesAbstraction.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesManager(IMapper mapper, IUnitOfWork unitOfWork,IConfiguration configuration,HttpClient httpClient, IHubContext<NotificationHub> hubContext, ISmsService smsService,ICacheRebository rebository) : ISerivcesManager
    {
        public ITicketPricesServices TicketPricesServices { get ; }=new TicketPricesServices(unitOfWork,mapper);
        public IFaultService FaultService { get; }=new FaultService(mapper, unitOfWork,hubContext,smsService);
        public ICongestionScheduleService CongestionScheduleService { get ;}= new CongestionScheduleService(mapper, unitOfWork, hubContext, smsService);
        public ILineNameServices LineNameServices { get; }= new LineNameServices(unitOfWork,mapper);
        public IStationsNameServices StationNameServices { get; }= new StationsNameServices(unitOfWork,mapper,httpClient,configuration);
        public IStation_CoordinatesServices Station_CoordinatesServices { get; }= new Station_CoordinateServices(unitOfWork,mapper);
        public IRushTimeServices RushTimeServices { get; }= new RushTimeServices(mapper,unitOfWork);
        public ICacheServices CacheServices { get; } = new CacheServices(rebository);
    }
}
