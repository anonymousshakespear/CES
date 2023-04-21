
using System.Collections;
using System.Drawing;
using System.Net.Mime;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RoutePlanning.Domain.Locations.Services.Interfaces;





namespace RoutePlanning.Domain.Locations.Services;
public class RoutingService: IRoutingService
{
    public static (int time, int price) FindShortestRoute(string cityFrom, string cityTo, string productCategory, int weight, string date="")
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
            var (timeAB, priceAB) = CalculateTimeAndCostOfSegmet( cityA,  cityB,  productCategory,  weight, date);
            if ((timeAB, priceAB) != (-1,-1))
            {
                locationA.AddConnection(locationB, 0, timeAB, priceAB, timeAB);
                locationB.AddConnection(locationA, 0, timeAB, priceAB, timeAB);
            }
        }


        var locationListOceanic = new List<Location>();
        var cityNamesOceanic = GetCityNames();
        var cityDictOceanic = new Dictionary<string, Location>();

        foreach (var cityName in cityNamesOceanic)
        {
            var location = new Location(cityName);
            locationListOceanic.Add(location);
            cityDictOceanic.Add(cityName, location);
        }

        var simpleConnectiosOceanic = GetPossibleConnectionsOceanic();

        foreach (var (cityA, cityB) in simpleConnectiosOceanic)
        {
            var locationA = cityDictOceanic[cityA];
            var locationB = cityDictOceanic[cityB];
            var (timeAB, priceAB) = CalculateTimeAndCostOfSegmetOceanic(cityA, cityB, productCategory, weight, date);
            if ((timeAB, priceAB) != (-1, -1))
            {
                locationA.AddConnection(locationB, 0, timeAB, priceAB, timeAB);
                locationB.AddConnection(locationA, 0, timeAB, priceAB, timeAB);
            }
        }

        foreach (var location in locationList)
        {
            var locationOC = cityDictOceanic[location.Name];

            if (location.Name.Equals(cityFrom) || location.Name.Equals(cityTo))
            {
                locationOC.AddConnection(location, 0, 0, 0,0);
                location.AddConnection(locationOC, 0, 0, 0, 0);
            }
            else
            {
                locationOC.AddConnection(location, 0, 10, 0, 10);
                location.AddConnection(locationOC, 0, 10, 0, 10);
            }
        }

        locationList.AddRange(locationListOceanic);

        var shortestDistanceService = new ShortestDistanceService(locationList.AsQueryable());

        // Act
        //var locationFrom = cityDict[cityFrom];
        //var locationTo = cityDict[cityTo];
        var locationFrom = cityDictOceanic[cityFrom];
        var locationTo = cityDictOceanic[cityTo];
        IEnumerable<Connection> path;
        try
        {
            path = shortestDistanceService.CalculateShortestPath(locationFrom, locationTo);
        }
        catch
        {
            return (-1, -1);
        }

        var (time, price) = ShortestDistanceService.CalculateTimePrice(path);
        return (time, price);
    }

    //private static  (int timeAB, int priceAB) CalculateTimeAndCostOfSegmetOceanicOld(string cityA, string cityB, string productCategory, int weight,string date)
    //{
    //    return (5, 40);
    //}

    private static (int timeAB, int priceAB) CalculateTimeAndCostOfSegmetOceanic(string cityA, string cityB, string productCategory, int weight, string date, int height=1, int width=1, int depth=1)
    {
        //var segment = new GetSegmentOceanCommand(cityA, cityA, Weight, Height, Width, Depth, "productCategory");


        return (5, 40);
    }

    private static List<(string, string)> GetPossibleConnectionsOceanic()
    {
        var connections = new List<(string, string)>
        {
            ("Cape Town","Walvis Bay"),
        };
        return connections;
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

    public static (int time, int price) CalculateTimeAndCostOfSegmet(string cityA, string cityB,string productCategory,int weight,string date="")
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
                    ("Cape Town",   "Walvis Bay",     3),
                    ("Cape Town",   "Saint Helena",       9),
                    ("Walvis Bay", "Slave Coast",    9),
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

        var (priceRate, timeRate) = GetRates(productCategory, weight, date);
        if ((priceRate, timeRate) == (-1, -1))
        {
            return (-1, -1);
        }

        var time = (int)(timeRate * segmentLength);
        var price = (int)(priceRate * segmentLength);

        return (time, price);
    }

    private static (double priceRate, double timeRate) GetRates(string productCategory, int weight, string date)
    {
        DateTime givenDateTime;
        try
        {
            givenDateTime = DateTime.Parse(date);
        }
        catch
        {
            givenDateTime = DateTime.Now;
        }

        double priceRate;
        double timeRate;
        if (givenDateTime.Month >= 11 || givenDateTime.Month <= 4) //Chek if we are between Novenmber and April 
        {
             priceRate = 8.0;
             timeRate = 12.0;

        }
        else
        {
            priceRate = 5.0;
            timeRate = 12.0;
        }



        if (productCategory.Contains("Weapons"))
        {
            priceRate = (priceRate* 1.2);
        }

        if (productCategory.Contains("Refrigerated goods"))
        {
            priceRate = (priceRate * 1.1);
        }

        if (productCategory.Contains("Live animals"))
        {
            priceRate = (priceRate * 1.25);
        }

        if (productCategory.Contains("Cautious parcels"))
        {
            return (-1,-1);
        }

        if (productCategory.Contains("Recorded Delivery"))
        {
            return (-1, -1);
        }





        return (priceRate, timeRate);
    }
}
