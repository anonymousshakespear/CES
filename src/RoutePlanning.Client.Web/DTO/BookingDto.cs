namespace RoutePlanning.Client.Web.DTO;

public class BookingDto
{
    public string Id { get; set; }

    public string Start { get; set; }

    public string End { get; set; }

    public int Cost { get; set; }

    public BookingDto(string id, string start, string end, int cost)
    {
        Id = id;
        Start = start;
        End = end;
        Cost = cost;
    }
}
