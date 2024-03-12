using Network.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Service.Interfaces
{
    public interface INetworkService
    {
        Response GetDownstreamCustomers(RootNode rootNode);
    }
}
