using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlanning.Domain.Locations.Services.Interfaces;

namespace RoutePlanning.Domain.Locations.Services;
internal class PortService : IPortService
{
    public void changePortStatus(string portID, bool portStatus)
    {
        throw new NotImplementedException();
    }

    public (string[] portID, string[] portName, bool portStatus) getPorts()
    {
        throw new NotImplementedException();
    }
}
