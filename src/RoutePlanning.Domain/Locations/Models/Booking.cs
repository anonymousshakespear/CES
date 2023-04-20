
namespace RoutePlanning.Domain.Locations.Models;
public class Booking
{
    private int ID;
    private int packageID;
    private string productCategory;
    private int userID;
    private int startingCityID;
    private int destinationCityID;
    private float height;
    private float weight;
    private float depth;
    private float length;
    private string remark;
    private string receiverInformation;
    private float cost;
    private DateTime bookingDate;
    private string status;
    private DateTime rowVersion;

    public Booking(int id, int packageId, string productCategory, int userId, int startingCityId, int destinationCityId, float height, float weight, float depth, float length, string remark, string receiverInformation, float cost, DateTime bookingDate, string status, DateTime rowVersion)
    {
        ID = id;
        packageID = packageId;
        this.productCategory = productCategory;
        userID = userId;
        startingCityID = startingCityId;
        destinationCityID = destinationCityId;
        this.height = height;
        this.weight = weight;
        this.depth = depth;
        this.length = length;
        this.remark = remark;
        this.receiverInformation = receiverInformation;
        this.cost = cost;
        this.bookingDate = bookingDate;
        this.status = status;
        this.rowVersion = rowVersion;
    }

    public Booking () {}
}
