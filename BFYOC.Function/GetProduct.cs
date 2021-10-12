using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC.Function
{
    public class GetProduct
    {
        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetProduct/{productId}")] HttpRequest req,
            [FromRoute] string productId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (string.IsNullOrWhiteSpace(productId))
            {
                return new BadRequestResult();
            }

            string response = $"The product name for your product id {productId} is Starfruit Explosion";

            return new OkObjectResult(response);
        }
    }
}
