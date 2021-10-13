using BFYOC.Function.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace BFYOC.Function.Extensions
{
    public class CosmosDBRatingStore : IRatingStore
    {
        private IConfiguration _configuration;

        private CosmosClient _client;

        private string _dbName;

        private string _containerName;


        public CosmosDBRatingStore(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbName = _configuration.GetSection("DatabaseName").Value;
            _containerName = _configuration.GetSection("RatingsContainerName").Value;

            string account = _configuration.GetSection("Account").Value;
            string key = _configuration.GetSection("Key").Value;

            _client = new CosmosClient(account, key);
        }

        public async Task StoreRatingAsync(RatingResponseModel model)
        {
            DatabaseResponse database = await _client.CreateDatabaseIfNotExistsAsync(_dbName);
            Container container = (await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/productId")).Container;

            try
            {
                await container.CreateItemAsync(model);
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
