using BFYOC.Function.Extensions;
using BFYOC.Function.Models;
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
    public class CreateRating
    {
        private ApiManager _apiManager;
        private CosmosDBRatingStore _ratingStore;

        public CreateRating(ApiManager apiManager, CosmosDBRatingStore ratingStore)
        {
            _apiManager = apiManager;
            _ratingStore = ratingStore;
        }

        [FunctionName("CreateRating")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **productId** of the product you are rating.")]
        [OpenApiRequestBody(contentType: "application/json", typeof(CreateRatingRequest), Required = true, Description = "The rating to create.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreateRating/{productId}")] CreateRatingRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            RatingResponseModel response;

            try
            {
                response = await _apiManager.CreateRatingAsync(req);
            }
            catch (ArgumentException e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            await _ratingStore.StoreRatingAsync(response);

            return new OkObjectResult(response);

        }
    }
}

