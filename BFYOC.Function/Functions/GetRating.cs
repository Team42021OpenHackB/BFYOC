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
    public class GetRating
    {
        private ApiManager _apiManager;
        private CosmosDBRatingStore _ratingStore;

        public GetRating(ApiManager apiManager, CosmosDBRatingStore ratingStore)
        {
            _apiManager = apiManager;
            _ratingStore = ratingStore;
        }

        [FunctionName("GetRating")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "ratingId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **id** of the rating you are getting.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "Get", Route = "GetRating/{ratingId}")] HttpRequest req,
            string ratingId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            RatingResponseModel result = await _ratingStore.GetRatingAsync(ratingId);
            if (result == null)
			{
                return new NotFoundObjectResult($"Not found (ratingId=={ratingId})");
			}
			else
			{
                return new OkObjectResult(result);
            }
        }
    }
}

