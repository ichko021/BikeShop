using BikeShop.DL.Interfaces;
using BikeShop.DTO.Configurations;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeShop.DL.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly IMongoCollection<Bike> _bikes;
        private readonly ILogger<BikeRepository> _logger;

        public BikeRepository(
            IOptionsMonitor<MongoDbConfig> mongoConfig,
            ILogger<BikeRepository> logger)
        {
            _logger = logger;

            var client = new MongoClient(
                mongoConfig.CurrentValue.MongoDbConnectionString);

            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);

            _bikes = database
                .GetCollection<Bike>($"{nameof(Bike)}s");


        }
        public async Task<Bike?>? AddBike(Bike bike)
        {
            await _bikes.InsertOneAsync(bike);

            return bike;
        }

        public async Task DeleteBikeById(string id)
        {
            var filter = Builders<Bike>.Filter
                    .Eq(b => b.id, id);

            await _bikes.DeleteOneAsync(filter);
        }

        public async Task<List<Bike>> GetAllBikes()
        {
            return await _bikes.Find(bike => true).ToListAsync();
        }

        public async Task<Bike?> GetBikeById(string id)
        {
            return await _bikes.Find(b => b.id == id).FirstOrDefaultAsync();
        }

        public async Task<Bike?> UpdateBikeById(string id, Bike bike)
        {
            var filter = Builders<Bike>.Filter
                .Eq(b => b.id, id);

            await _bikes.ReplaceOneAsync(filter, bike);

            return bike;
        }
    }
}
