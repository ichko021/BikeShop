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
        public Bike? AddBike(Bike bike)
        {
            try
            {
                _bikes.InsertOne(bike);          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot add new bike {ex.Message} | {ex.StackTrace}");
                throw;
            }

            return bike;
        }

        public void DeleteBikeById(string id)
        {
            var filter = Builders<Bike>.Filter
                    .Eq(b => b.id, id);

            try
            {
                _bikes.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                  $"Cannot delete bike {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public List<Bike> GetAllBikes()
        {
            return _bikes.Find(bike => true).ToList();
        }

        public Bike? GetBikeById(string id)
        {
            try
            {
                return _bikes.AsQueryable()
                        .Where(b => b.id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                 $"Cannot fetch bike by id. {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }

        public Bike? UpdateBikeById(string id, Bike bike)
        { 
            var filter = Builders<Bike>.Filter
                .Eq(b => b.id, id);

            try
            {
                _bikes.ReplaceOne(filter, bike);

                return bike;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot update bike {ex.Message} | {ex.StackTrace}");
                throw;
            }
        }
    }
}
