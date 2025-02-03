using BikeShop.DL.Interfaces;
using BikeShop.DTO.Configurations;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeShop.DL.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly IMongoCollection<Part> _parts;
        private readonly ILogger<PartRepository> _logger;

        public PartRepository(
            IOptionsMonitor<MongoDbConfig> mongoConfig,
            ILogger<PartRepository> logger)
        {
            _logger = logger;

            var client = new MongoClient(
                mongoConfig.CurrentValue.MongoDbConnectionString);

            var database = client.GetDatabase(
                mongoConfig.CurrentValue.DatabaseName);

            _parts = database
                .GetCollection<Part>($"{nameof(Part)}s");


        }
        public Part AddPart(Part part)
        {
            _parts.InsertOne(part);

            return part;
        }

        public void DeletePartById(string id)
        {
            var filter = Builders<Part>.Filter
                    .Eq(p => p.id, id);

            _parts.DeleteOne(filter);
        }

        public List<Part> GetAllParts()
        {
            return _parts.Find(part => true).ToList();
        }

        public Part? GetPartById(string id)
        {
            return _parts.AsQueryable()
                        .Where(p => p.id == id).FirstOrDefault();
        }

        public Part? UpdatePartById(string id, Part part)
        {
            var filter = Builders<Part>.Filter
                .Eq(b => b.id, id);

            _parts.ReplaceOne(filter, part);

            return part;
        }
    }
}
