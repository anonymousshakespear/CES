
using System.ComponentModel.DataAnnotations.Schema;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations.Models;
[Table("Booking_T")]
public class Booking : AggregateRoot<Booking>
{
    public int PackageID { get; set; }
    public string ProductCategory { get; set; }
    public Guid UserID { get; set; }
    public string StartingCity { get; set; }
    public string DestinationCity { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public float Depth { get; set; }
    public float Length { get; set; }
    public string Remark { get; set; }
    public string ReceiverInformation { get; set; }
    public float Cost { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; }

    public Booking(int packageID, string productCategory, Guid userID, string startingCity, string destinationCity, float height, float weight, float depth, float length, string remark, string receiverInformation, float cost, DateTime bookingDate, string status)
    {
        PackageID = packageID;
        ProductCategory = productCategory;
        UserID = userID;
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
