using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services.Interfaces;

public interface IPortService
{
    void changePortStatus(string portID,bool portStatus);
    (string[] portID, string[] portName, bool portStatus) getPorts();
}
