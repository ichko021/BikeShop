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
        public void AddPart(Part part)
        {
            if (part == null)
            {
                _logger.LogError("Part object is null");
                return;
            }

            try
            {
                _parts.InsertOne(part);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot add new part {ex.Message} | {ex.StackTrace}");
            }
        }

        public void DeletePartById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("PartId is null or empty.");
                return;
            }

            var filter = Builders<Part>.Filter
                    .Eq(b => b.id, id);

            try
            {
                _parts.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                  $"Cannot delete part {ex.Message} | {ex.StackTrace}");
            }
        }

        public List<Part> GetAllParts()
        {
            return _parts.Find(part => true).ToList();
        }

        public Part? GetPartById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("PartId is null or empty.");
                return null;
            }

            var query = _parts.AsQueryable()
                        .Where(p => p.id == id).FirstOrDefault();

            return query;
        }

        public void UpdatePartById(string id, Part part)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("PartId is null or empty.");
                return;
            }

            var filter = Builders<Part>.Filter
                .Eq(b => b.id, id);

            var update = Builders<Part>.Update
                .Set(p => p.partName, part.partName)
                .Set(p => p.partSpec, part.partSpec);

            try
            {
                _parts.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   $"Cannot update part. {ex.Message} | {ex.StackTrace}");
            }
        }
    }
}
