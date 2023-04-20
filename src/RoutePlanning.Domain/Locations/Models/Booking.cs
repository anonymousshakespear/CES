
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class Booking : AggregateRoot<Booking>
{
    public int PackageID { get; set; }
    public string ProductCategory { get; set; }
    public int UserID { get; set; }
    public int StartingCityID { get; set; }
    public int DestinationCityID { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public float Depth { get; set; }
    public float Length { get; set; }
    public string Remark { get; set; }
    public string ReceiverInformation { get; set; }
    public float Cost { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; }

    public Booking(int packageID, string productCategory, int userID, int startingCityID, int destinationCityID, float height, float weight, float depth, float length, string remark, string receiverInformation, float cost, DateTime bookingDate, string status)
    {
        PackageID = packageID;
        ProductCategory = productCategory;
        UserID = userID;
        StartingCityID = startingCityID;
        DestinationCityID = destinationCityID;
        Height = height;
        Weight = weight;
        Depth = depth;
        Length = length;
        Remark = remark;
        ReceiverInformation = receiverInformation;
        Cost = cost;
        BookingDate = bookingDate;
        Status = status;
    }
}
