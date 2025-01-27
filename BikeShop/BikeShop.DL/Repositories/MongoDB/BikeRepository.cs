using BikeShop.DL.Interfaces;
using BikeShop.DTO;
using BikeShop.DTO.Configurations;
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
        public void AddBike(Bike bike)
        {
            if (bike == null)
            {
                _logger.LogError("Bike object is null");
                return;
            }

            try
            {
                bike.id = Guid.NewGuid().ToString();

                _bikes.InsertOne(bike);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot add new bike {ex.Message} | {ex.StackTrace}");
            }
        }

        public void DeleteBikeById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("BikeId is null or empty.");
            }

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
            }
            //foreach (Bike b in InMemoryDB.listOfBikes)
            //{
            //    if(b.id == id)
            //    {
            //        InMemoryDB.listOfBikes.Remove(b);
            //    }
            //}
        }

        public List<Bike> GetAllBikes()
        {
            return _bikes.Find(bike => true).ToList();
        }

        public Bike? GetBikeById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("BikeId is null or empty.");
            }

            var query = _bikes.AsQueryable()
                        .Where(b => b.id == id).FirstOrDefault();

            return query;
            //foreach (Bike b in InMemoryDB.listOfBikes)
            //{
            //    if (b.id == id)
            //    {
            //        return InMemoryDB.listOfBikes[b.id];
            //    }
            //}

            //return null;
        }

        public void UpdateBikeById(string id, Bike bike)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("BikeId is null or empty.");
            }

            var filter = Builders<Bike>.Filter
                .Eq(b => b.id, id);

            var update = Builders<Bike>.Update
                .Set(b => b.brand, bike.brand)
                .Set(b => b.model, bike.model)
                .Set(b => b.price, bike.price)
                .Set(b => b.availabilityInStore, bike.availabilityInStore);

            try
            {
                _bikes.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot update bike {ex.Message} | {ex.StackTrace}");
            }
            

            //foreach (Bike b in InMemoryDB.listOfBikes)
            //{
            //    if (b.id == id)
            //    {
            //        InMemoryDB.listOfBikes[id] = bike;
            //    }
            //}
        }
    }
}
