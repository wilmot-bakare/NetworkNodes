using Microsoft.Extensions.Logging;
using Network.Core.Models;
using Network.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Service.Implementations
{
    public class NetworkService : INetworkService
    {
        public readonly ILogger<NetworkService> _logger;

        public NetworkService(ILogger<NetworkService> logger)
        {
            _logger = logger;
        }
        public async Task<Response> GetDownstreamCustomers(RootNode rootNode)
        {
            _logger.LogInformation($"GetDownstreamCustomers service: Selected node:{rootNode.SelectedNode}");
            try
            {
                Response reponse = new Response();
                HashSet<int> visited = new HashSet<int>();
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(rootNode.SelectedNode);
                visited.Add(rootNode.SelectedNode);
                while (queue.Count > 0)
                {
                    int currentNode = queue.Dequeue();
                    reponse.DownstreamCustomers += await GetCustomersForNode(rootNode.Network, currentNode);

                    foreach (var branch in rootNode.Network.Branches)
                    {
                        if (branch.StartNode == currentNode && !visited.Contains(branch.EndNode))
                        {
                            queue.Enqueue(branch.EndNode);
                            visited.Add(branch.EndNode);
                        }
                    }
                }
                _logger.LogInformation($"GetDownstreamCustomers service: totalCustomers :{reponse.DownstreamCustomers}");
                return reponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetCustomersForNode(NetworkData networkData, int node)
        {
            foreach (var customer in networkData.Customers)
            {
                if (customer.Node == node)
                {
                    return customer.NumberOfCustomers;
                }
            }
            return 0;
        }
    }
}
