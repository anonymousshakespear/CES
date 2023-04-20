
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
public class Booking : AggregateRoot<Booking>
{
    public int PackageID { get; set; }
    public string ProductCategory { get; set; }
    public UserProfile User { get; set; }
    public City StartingCity { get; set; }
    public City DestinationCity { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public float Depth { get; set; }
    public float Length { get; set; }
    public string Remark { get; set; }
    public string ReceiverInformation { get; set; }
    public float Cost { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; }

    public Booking(int packageId, string productCategory, UserProfile user, City startingCity, City destinationCity, float height, float weight, float depth, float length, string remark, string receiverInformation, float cost, DateTime bookingDate, string status)
    {
        PackageID = packageId;
        ProductCategory = productCategory;
        User = user;
        StartingCity = startingCity;
        DestinationCity = destinationCity;
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
