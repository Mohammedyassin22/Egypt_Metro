using AutoMapper;
using Domain.Contracts;
using Domain.Modules;
using ServicesAbstraction;
using Shared;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class StationsNameServices : IStationsNameServices
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly HttpClient _httpClient;
    private readonly string _googleApiKey;

        public StationsNameServices(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient, IConfiguration config)
    {
        this.unitOfWork = unitOfWork;
       this.mapper = mapper;
        _httpClient = httpClient;
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "EgyptMetroApp/1.0 (contact: your-email@example.com)");
        }
    }

        public async Task<Station_NameDto> AddStationWithCoordinatesAsync(string stationName, CancellationToken cancellationToken = default)
            {
            if (string.IsNullOrWhiteSpace(stationName))
                throw new ArgumentException("stationName must be provided", nameof(stationName));

            string addressQuery = Uri.EscapeDataString($"{stationName}, Cairo, Egypt");
            string apiUrl = $"https://nominatim.openstreetmap.org/search?format=json&q={addressQuery}";

            double lat, lng;

            using var response = await _httpClient.GetAsync(apiUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new InvalidOperationException($"OpenStreetMap API returned {(int)response.StatusCode}: {body}");
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using (var doc = JsonDocument.Parse(json))
            {
                var results = doc.RootElement;
                if (results.GetArrayLength() == 0)
                    throw new InvalidOperationException($"Couldn't find coordinates for '{stationName}' using OpenStreetMap.");

                var first = results[0];
                lat = double.Parse(first.GetProperty("lat").GetString());
                lng = double.Parse(first.GetProperty("lon").GetString());
            }

            var station = new Station_Name
            {
                StationName = stationName,
                Coordinates = new Station_Coordinates
                {
                    Latitude = lat,
                    Longitude = lng
                }
            };

            var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
            await stationRepo.AddAsync(station);
            await unitOfWork.SaveChangeAsync(cancellationToken);

            var dto = mapper.Map<Station_NameDto>(station);
            return dto;
        }
    
        public async Task<IEnumerable<Station_NameDto>> GetAllStationsAsync()
        {
            var stations = await unitOfWork .GetRepository<Station_Name,int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<Station_Name>, IEnumerable<Station_NameDto>>(stations);
            return result;
        }
        public async Task<string> GetStationNameAsync(string name)
        {
            var station = await unitOfWork.GetRepository<Station_Name, int>().GetByConditionAsync(x=>x.StationName==name);
            if (station is null)
            {
                return null;
            }
            var result = mapper.Map<Station_NameDto>(station);
            return result.StationName;

        }
    }

