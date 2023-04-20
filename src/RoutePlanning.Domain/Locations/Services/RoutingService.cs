
using System.Collections;
using System.Drawing;
using RoutePlanning.Domain.Locations.Services.Interfaces;

namespace RoutePlanning.Domain.Locations.Services;
public class RoutingService: IRoutingService
{
    public static (int time, int price) FindShortestRoute(string cityFrom, string cityTo, ProductCategory productCategory, int weight)
    {

        var locationList = new List<Location>();

        var cityNames = GetCityNames();
        var cityDict = new Dictionary<string, Location>();

        foreach (var cityName in cityNames)
        {
            var location = new Location(cityName);
            locationList.Add(location);
            cityDict.Add(cityName, location);
        }

        var simpleConnectios = GetPossibleConnections();

        foreach (var (cityA,cityB) in simpleConnectios)
        {
            var locationA = cityDict[cityA];
            var locationB = cityDict[cityB];
            var (timeAB, priceAB) = CalculateTimeAndCostOfSegmet( cityA,  cityB,  productCategory,  weight);
            if ((timeAB, priceAB) != (-1,-1))
            {
                locationA.AddConnection(locationB, 0, timeAB, priceAB, timeAB);
                locationB.AddConnection(locationA, 0, timeAB, priceAB, timeAB);
            }
        }

        var shortestDistanceService = new ShortestDistanceService(locationList.AsQueryable());

        // Act
        var locationFrom = cityDict[cityFrom];
        var locationTo = cityDict[cityTo];

        var path = shortestDistanceService.CalculateShortestPath(locationFrom, locationTo);

        var (time, price) = ShortestDistanceService.CalculateTimePrice(path);
        return (time, price);
    }

    private static List<string> GetCityNames()
    {
        var dubleCityNames = new List<(string, string)>
        {
            ("Addis Abeba", "Addis Ababa"), ("Bahr el Ghazal", "Bahr el Ghazal"), ("Cairo", "Cairo"), ("Dakar", "Dakar"), ("Darfur", "Darfur"), ("Drakbergen", "Dragon Mountains"), ("Guldkysten", "Gold Coast"), ("Kabalo", "Kabalo"), ("De Kanariske Øer", "Canary Islands"), ("Hvalbugten", "Walvis Bay"), ("Kap Guardafui", "Cape Guardafui"), ("Kap St. Marie", "Cape St. Marie"), ("Kapstaden", "Cape Town"), ("Kongo", "Congo"), ("Luanda", "Luanda"), ("Marrakech", "Marrakech"), ("Moçambique", "Mozambique"), ("Omdurman", "Omdurman"), ("Sahara", "Sahara"), ("Sankta Helena", "Saint Helena"), ("Sierra Leone", "Sierra Leone"), ("Beninbugten", "Bight of Benin"), ("Suakin", "Suakin"), ("Tamatave", "Tamatave"), ("Tangen", "Tangier"), ("Timbuktu", "Timbuktu"), ("Tripolis", "Tripoli"), ("Tunis", "Tunis"), ("Victoriafaldet", "Victoria Falls"), ("Victoriasøen", "Lake Victoria"), ("Wadai", "Wadai"), ("Zanzibar", "Zanzibar"),("Tangiers","Tangiers"),("Slave kysten","Slave Coast")

        };

        var cityNames = new List<string>();

        foreach (var (cityNameDanish, cityNameEnglish) in dubleCityNames)
        {
            cityNames.Add(cityNameEnglish);
        }

        return cityNames;
        
    }

    private static string TranslateCityNameEnglishToDanish(string cityName)
    {
        var dubleCityNames = new List<(string, string)>
        {
            ("Addis Abeba", "Addis Ababa"), ("Bahr el Ghazal", "Bahr el Ghazal"), ("Cairo", "Cairo"), ("Dakar", "Dakar"), ("Darfur", "Darfur"), ("Drakbergen", "Dragon Mountains"), ("Guldkysten", "Gold Coast"), ("Kabalo", "Kabalo"), ("De Kanariske Øer", "Canary Islands"), ("Hvalbugten", "Walvis Bay"), ("Kap Guardafui", "Cape Guardafui"), ("Kap St. Marie", "Cape St. Marie"), ("Kapstaden", "Cape Town"), ("Kongo", "Congo"), ("Luanda", "Luanda"), ("Marrakech", "Marrakech"), ("Moçambique", "Mozambique"), ("Omdurman", "Omdurman"), ("Sahara", "Sahara"), ("Sankta Helena", "Saint Helena"), ("Sierra Leone", "Sierra Leone"), ("Beninbugten", "Bight of Benin"), ("Suakin", "Suakin"), ("Tamatave", "Tamatave"), ("Tangen", "Tangier"), ("Timbuktu", "Timbuktu"), ("Tripolis", "Tripoli"), ("Tunis", "Tunis"), ("Victoriafaldet", "Victoria Falls"), ("Victoriasøen", "Lake Victoria"), ("Wadai", "Wadai"), ("Zanzibar", "Zanzibar"),("Tangiers","Tangiers"),("Slave kysten","Slave Coast")

        };

        var cityNamesDict = new Dictionary<string, string>();

        foreach (var (cityNameDanish, cityNameEnglish) in dubleCityNames)
        {
            cityNamesDict.Add(cityNameEnglish, cityNameDanish);
        }

        return cityNamesDict[cityName];

    }

    public void FindShortestRoute()
    {
        throw new NotImplementedException();
    }

    public void FindCheapestRoute()
    {
        throw new NotImplementedException();
    }

    public void FindPreferableRoute()
    {
        throw new NotImplementedException();
    }

    public static List<(string, string)>  GetPossibleConnections()
    {
        var connections = new List<(string, string)>
        {
            ("Canary Islands","Tangiers"),
            ("Canary Islands",  "Dakar"),
            ("Tangier", "Tunis"),
            ("Tunis", "Cairo"),
            ("Cairo", "Suakin"),
            ("Suakin", "Cape Guardafui"),
            ("Cape Guardafui", "Tamatave"),
            ("Cape Guardafui","Mozambique"),
            ("Mozambique", "Cape St. Marie"),
            ("Cape St. Marie", "Cape Town"),
            ("Cape Town",   "Walvis Bay"),
            ("Cape Town",   "Saint Helena"),
            ("Walvis Bay", "Slave Coast"),
            ("Slave Coast", "Gold Coast"),
            ("Saint Helena",    "Dakar"),
            ("Saint Helena",    "Sierra Leone"),
            ("Sierra Leone",    "Dakar"),
        };
        

        return connections;
    }

    public static (int time, int price) CalculateTimeAndCostOfSegmet(string cityA, string cityB,ProductCategory productCategory,int weight)
    {
        
        var connections = new List<(string, string,int)>
                { 
                    ("Canary Islands","Tangiers",3),
                    ("Canary Islands",  "Dakar",5),
                    ("Tangier", "Tunis",      3),
                    ("Tunis", "Cairo",  5),
                    ("Cairo", "Suakin",  4),
                    ("Suakin", "Cape Guardafui",  4),
                    ("Cape Guardafui","Tamatave",  8),
                    ("Cape Guardafui","Mozambique",     8),
                    ("Mozambique", "Cape St. Marie",     3),
                    ("Cape St. Marie", "Cape Town",      8),
                    ("Cape Town",   "Hvalbugten",     3),
                    ("Cape Town",   "Saint Helena",       9),
                    ("Hvalbugten", "Slave Coast",    9),
                    ("Slave Coast", "Gold Coast",     4),
                    ("Sierra Leone", "Gold Coast",     4),
                    ("Hvalbugten", "Gold Coast",11),
                    ("Saint Helena",    "Dakar",    10),
                    ("Saint Helena",    "Sierra Leone",11),
                    ("Sierra Leone",    "Dakar" ,    3),
                };
        var segmentLength = -1;
        foreach (var (cityFrom ,cityTo ,lenght) in connections)
        {
            if (cityA.Equals(cityFrom) && cityB.Equals(cityTo) )
            {
                segmentLength = lenght;
                break;
            }
            else if (cityB.Equals(cityFrom) && cityA.Equals(cityTo))
            {
                segmentLength = lenght;
                break;
            }
        }

        if (segmentLength == -1)
        {
            return (-1, -1);
        }

        var (priceRate, timeRate) = GetRates(productCategory, weight);
        var time = timeRate * segmentLength;
        var price = priceRate * segmentLength;

        return (time, price);
    }

    private static (int priceRate,int timeRate) GetRates(ProductCategory productCategory, int weight)
    {
        var priceRate = 8;
        var timeRate = 12;
     
        return (priceRate, timeRate);
    }
}
