using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Poc1;

namespace My.Functions
{
    public static class SelectUser
    {
        [FunctionName("SelectUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb",
                Id = "{Query.id}",
                PartitionKey = "{Query.id}")
               ] Users users,
            ILogger log)
        {
            var id = req.Query["id"];
            if (users is null)
            {
                return new NotFoundObjectResult("{\"status\":\"not found\"}");
            }
            return new OkObjectResult(users);
        }
    }
}
