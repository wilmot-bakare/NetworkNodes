using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Network.Core.Models;
using Network.Service.Interfaces;

namespace Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        public readonly ILogger<NetworkController> _logger;
        public readonly INetworkService _service;
        public NetworkController(INetworkService service, ILogger<NetworkController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public IActionResult GetDownstreamCustomers(RootNode rootNode)
        {
            if (rootNode == null || rootNode.Network == null || rootNode.Network.Branches.Count < 1 || rootNode.Network.Customers.Count < 1 || rootNode.SelectedNode == 0)
            {
                return BadRequest("Kindly check your that branches,customers and selected node have values and try again.");
            }
            _logger.LogInformation($"GetDownstreamCustomers Request:Selected node is {rootNode.SelectedNode}");
            var response = _service.GetDownstreamCustomers(rootNode);
            return Ok(response);

        }
    }
}
