using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Poc1;

namespace My.Functions
{
    public static class CreateUser
    {
        [FunctionName("CreateUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb")] IAsyncCollector<dynamic> userStore,
            ILogger log)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JsonConvert.DeserializeObject<Users>(content);
            userObject.id = System.Guid.NewGuid().ToString();
            log.LogInformation(userObject.ToString());


            if (userObject.FirstName.Equals(null) || userObject.LastName.Equals(null) || userObject.SubscriberID.Equals(null))
            {
                return new BadRequestResult();
            }

            await userStore.AddAsync(userObject);

            var response = $"Added {userObject.FirstName} {userObject.LastName} id: {userObject.id}";

            return new OkObjectResult(response);
        }
    }
}
