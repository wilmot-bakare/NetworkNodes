using Microsoft.Extensions.Logging;
using Moq;
using Network.API;
using Network.Core.Models;
using Network.Service.Implementations;
using Network.Service.Interfaces;
using Newtonsoft.Json;

namespace Network.Tests
{
    [TestClass]
    public class NetworkTests
    {

        private ILogger<NetworkService> _logger;
        private INetworkService _networkService;
        
        [TestInitialize]
        public void Initialize()
        {
            var loggerMock = new Mock<ILogger<NetworkService>>();
            _networkService = new NetworkService(loggerMock.Object);
        }

        [TestMethod]
        public void GetDownstreamCustomers_Should_Return_Customer()
        {
            // Arrange

            string jsonData = @"{
            ""network"": {
                ""branches"": [
                    {""startNode"": 10, ""endNode"": 20},
                    {""startNode"": 20, ""endNode"": 30},
                    {""startNode"": 30, ""endNode"": 50},
                    {""startNode"": 50, ""endNode"": 60},
                    {""startNode"": 50, ""endNode"": 90},
                    {""startNode"": 60, ""endNode"": 40},
                    {""startNode"": 70, ""endNode"": 80}
                ],
                ""customers"": [
                    {""node"": 30, ""numberOfCustomers"": 8},
                    {""node"": 40, ""numberOfCustomers"": 3},
                    {""node"": 60, ""numberOfCustomers"": 2},
                    {""node"": 70, ""numberOfCustomers"": 1},
                    {""node"": 80, ""numberOfCustomers"": 3},
                    {""node"": 90, ""numberOfCustomers"": 5}
                ]
            },
            ""selectedNode"": 50
        }";

            RootNode rootNode = JsonConvert.DeserializeObject<RootNode>(jsonData);

            // Act
            var response = _networkService.GetDownstreamCustomers(rootNode);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(10, response.DownstreamCustomers);
        }

        [TestMethod]
        public  void GetDownstreamCustomers_Throws_Exception()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<NetworkService>>();
            var networkService = new NetworkService(loggerMock.Object);

            var rootNode = new RootNode();

            // Act & Assert
             Assert.ThrowsException<NullReferenceException>(() => networkService.GetDownstreamCustomers(rootNode));
        }
    }
}