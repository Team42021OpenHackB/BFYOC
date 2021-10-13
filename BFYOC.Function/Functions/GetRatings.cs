using BFYOC.Function.Extensions;
using BFYOC.Function.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BFYOC.Function.Functions
{
    public class GetRatings
    {
        private ApiManager _apiManager;
        private CosmosDBRatingStore _ratingStore;

        public GetRatings(ApiManager apiManager, CosmosDBRatingStore ratingStore)
        {
            _apiManager = apiManager;
            _ratingStore = ratingStore;
        }

        [FunctionName("GetRatings")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **id** of the user you are searching ratings by.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "Get", Route = "GetRatings/{userId}")] HttpRequest req,
            string userId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            RatingResponseModel[] results = await _ratingStore.GetRatingsAsync(userId);
            if (results == null || results.Length == 0)
            {
                return new NotFoundObjectResult($"Not Found (userId=={userId})");
            }
            else
			{
                return new OkObjectResult(results);
			}
        }
    }
}

