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
using Services.Specifcation;
using Domain.Exception;
using Presistense.Rebository;

public class StationsNameServices : IStationsNameServices
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly HttpClient _httpClient;
    private readonly string _googleApiKey;
    private readonly ICacheRebository _cacheRebository;
        public StationsNameServices(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient, IConfiguration config, ICacheRebository _cacheRebository)
    {
        this.unitOfWork = unitOfWork;
       this.mapper = mapper;
        _httpClient = httpClient;
        this._cacheRebository = _cacheRebository;
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

         public async Task<Station_NameDto> DeleteAsync(int id)
    {
        var faultrepo = unitOfWork.GetRepository<Station_Name, int>();
        var faultEntity = await faultrepo.GetAsync(id);
        if (faultEntity == null)
            throw new KeyNotFoundException($"No fault found with ID {id}.");
        faultrepo.Delete(faultEntity);
        await unitOfWork.SaveChangeAsync();
        var resultDto = mapper.Map<Station_NameDto>(faultEntity);
        return resultDto;
    }

    public async Task<IEnumerable<Station_NameDto>> GetAllStationsAsync(string? LineName)
    {
        string cacheKey = string.IsNullOrEmpty(LineName) ? "stations_all" : $"stations_{LineName.ToLower()}";

        var cachedData = await _cacheRebository.GetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var stationsFromCache = JsonSerializer.Deserialize<IEnumerable<Station_NameDto>>(cachedData);
            if (stationsFromCache != null && stationsFromCache.Any())
                return stationsFromCache;
        }

        var stationRepo = unitOfWork.GetRepository<Station_Name, int>();
        var spec = string.IsNullOrEmpty(LineName)
            ? new StationNameSpecification()
            : new StationNameSpecification(LineName);

        var stations = await stationRepo.GetAllAsync(spec);

        if (stations == null || !stations.Any())
            return Enumerable.Empty<Station_NameDto>();

        var result = mapper.Map<IEnumerable<Station_NameDto>>(stations);

        await _cacheRebository.SetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(10));

        return result;
   }

    public async Task<string> GetStationNameAsync(string name)
        {
            var station = await unitOfWork.GetRepository<Station_Name, int>().GetByConditionAsync(x=>x.StationName==name);
            if (station is null)
            {
            throw new NotFoundStationName(name);
        }
            var result = mapper.Map<Station_NameDto>(station);
            return result.StationName;

        }
    }

