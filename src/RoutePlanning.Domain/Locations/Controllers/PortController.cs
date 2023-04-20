namespace RoutePlanning.Domain.Locations.Controllers;
public sealed class PortController
{
    public String changePortStatus(Int32 portID, Boolean portStatus)
    {
        try
        {
            City city = cityRepository.getCityByID(portID);
        }
        catch (Exception ex)
        {
            return "No City with that ID";
        }
        city.setStatus(portStatus);
        cityRepository.updateCity(city);
        return "Updated successfully";
    }
}
