using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using WatchPortalFunction.Model;
using WatchPortalFunction.Repository;

namespace WatchPortalFunction.Functions
{
    public class WatchInfo
    {
        private IWatchRepository _watchRepository;

        public WatchInfo(IWatchRepository watchRepository)
        {
            _watchRepository = watchRepository;
        }

        [FunctionName("WatchInfo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the model id from the query string
            string model = req.Query["model"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            model = model ?? data?.model;

            // If the user specified a model id, find the details of the model of watch
            if (model != null)
            {
                Watch watch = _watchRepository.GetWatch(model);

                if (watch != null)
                    return new OkObjectResult(watch);
                else
                    return new NotFoundObjectResult("This model doesn't exist");
            }
            return new BadRequestObjectResult("Please provide a watch model in the query string or in the request body");
        }
    }
}
