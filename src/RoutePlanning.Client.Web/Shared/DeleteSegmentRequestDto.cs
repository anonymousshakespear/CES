namespace RoutePlanning.Client.Web.Shared;

public class DeleteSegmentRequestDto
{
    public string confirmationID = "";

    public DeleteSegmentRequestDto(string confirmationId) { 
        confirmationID = confirmationId;
    }
}
