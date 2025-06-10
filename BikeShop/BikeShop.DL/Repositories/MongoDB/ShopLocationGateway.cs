using BikeShop.DL.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop.DL.Repositories.MongoDB
{
    public class ShopLocationGateway : IShopLocationGateway
    {
        private readonly RestClient _client;

        public ShopLocationGateway()
        {
            var options = new RestClientOptions("https://localhost:7246");

            _client = new RestClient(options);
        }

        public async Task<string> GetAllLocations()
        {
            var request = new RestRequest($"/location", Method.Get);

            var response = await _client.ExecuteAsync(request);

            return response.Content.ToString();
        }
    }
}
