using BikeShop.DL.Interfaces;
using BikeShop.DTO.Configurations;
using BikeShop.DTO.POCO;
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

        public async Task<Part> AddPart(Part part)
        {
            await _parts.InsertOneAsync(part);
            return part;
        }

        public async Task DeletePartById(string id)
        {
            var filter = Builders<Part>.Filter.Eq(p => p.id, id);
            await _parts.DeleteOneAsync(filter);
        }

        public async Task<List<Part>> GetAllParts()
        {
            var result = await _parts.FindAsync(part => true);
            return await result.ToListAsync();
        }

        public async Task<Part?> GetPartById(string id)
        {
            var filter = Builders<Part>.Filter.Eq(p => p.id, id);
            var result = await _parts.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<Part?> UpdatePartById(string id, Part part)
        {
            var filter = Builders<Part>.Filter.Eq(b => b.id, id);
            await _parts.ReplaceOneAsync(filter, part);
            return part;
        }
    }
}
