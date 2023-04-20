namespace RoutePlanning.Client.Web.Shared;

public class GetSegmentRequestDto
{
    public string start = "";
    public string end = "";
    public double weight = 0.0;
    public double height = 0.0;
    public double width = 0.0;
    public double depth = 0.0;
    public string type = "STANDARD";

    public GetSegmentRequestDto() { }
}
