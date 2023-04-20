namespace RoutePlanning.Client.Web.Shared;

public class BookSegmentRequestDto : GetSegmentRequestDto
{
    public DateTime time = DateTime.Now;
    public string packageId = "";

    public BookSegmentRequestDto() { }
}
