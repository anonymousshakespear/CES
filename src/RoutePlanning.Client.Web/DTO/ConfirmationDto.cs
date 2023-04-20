public class ConfirmationDto
{
    public ConfirmationDto(string confirmationId, int cost, int time)
    {
        ConfirmationId = confirmationId;
        Cost = cost;
        Time = time;
    }

    public string ConfirmationId { get; set; }

    public int Cost { get; set; }

    public int Time { get; set; }
}
