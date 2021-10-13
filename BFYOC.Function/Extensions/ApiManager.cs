using BFYOC.Function.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BFYOC.Function.Extensions
{

    public class ApiManager
    {
        private HttpClient _httpClient;

        public ApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateUserId(string userId)
        {
            string uri = $"https://serverlessohapi.azurewebsites.net/api/GetUser?userId={userId}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ValidateProductId(string productId)
        {
            string uri = $"https://serverlessohapi.azurewebsites.net/api/GetProduct?productId={productId}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            return response.IsSuccessStatusCode;
        }

        public async Task<RatingResponseModel> CreateRatingAsync(CreateRatingRequest request)
        {
            if (!await ValidateUserId(request.UserId))
            {
                throw new ArgumentException($"User {request.UserId} not found.");
            }

            if (!await ValidateProductId(request.ProductId))
            {
                throw new ArgumentException($"Product {request.ProductId} not found.");
            }

            if (request.Rating.GetValueOrDefault() < 0 || request.Rating.GetValueOrDefault() > 5)
            {
                throw new ArgumentException($"Rating must be between 0 and 5");
            }

            var responseModel = new RatingResponseModel
            {
                Id = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.UtcNow,
                UserId = request.UserId,
                ProductId = request.ProductId,
                LocationName = request.LocationName,
                Rating = request.Rating,
                UserNotes = request.UserNotes
            };

            return responseModel;
        }
    }
}
