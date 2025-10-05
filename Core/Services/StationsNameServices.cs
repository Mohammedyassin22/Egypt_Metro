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
        _googleApiKey = config["GoogleAPI:MapsApiKey"] ?? throw new ArgumentNullException("GoogleAPI:MapsApiKey");

    }
        public async Task<Station_NameDto> AddStationWithCoordinatesAsync(string stationName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(stationName))
            throw new ArgumentException("stationName must be provided", nameof(stationName));

        string addressQuery = Uri.EscapeDataString($"{stationName}, Cairo, Egypt");
        string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={addressQuery}&key={_googleApiKey}";

        double lat, lng;

        using var response = await _httpClient.GetAsync(apiUrl, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException($"Google API returned {(int)response.StatusCode}: {body}");
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using (var doc = JsonDocument.Parse(json))
        {
            if (!doc.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                throw new InvalidOperationException($"Google API couldn't find coordinates for '{stationName}'");

            var first = results[0];
            if (!first.TryGetProperty("geometry", out var geom) ||
                !geom.TryGetProperty("location", out var loc) ||
                !loc.TryGetProperty("lat", out var latProp) ||
                !loc.TryGetProperty("lng", out var lngProp))
            {
                throw new InvalidOperationException("Unexpected Google API response shape.");
            }

            lat = latProp.GetDouble();
            lng = lngProp.GetDouble();
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
        public async Task<Station_NameDto> AddTicketPriceAsync(Station_NameDto newname)
        {
            var priceEntity = mapper.Map<Station_Name>(newname);

            var repository = unitOfWork.GetRepository<Station_Name, int>();

            await repository.AddAsync(priceEntity);

            await unitOfWork.SaveChangeAsync();

            var resultDto = mapper.Map<Station_NameDto>(priceEntity);

            return resultDto;
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

