namespace RoutePlanning.Client.Web.DTO;

public class BookingDto
{
    public string Status { get; set; }

    public BookingDto(string status)
    {
        Status = status;
    }
}
