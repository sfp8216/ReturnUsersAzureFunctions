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
using System.Collections.Generic;

namespace My.Functions
{
    public static class ReturnUsers
    {
        [FunctionName("ReturnUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb",
                SqlQuery = "SELECT * FROM Users")
               ] IEnumerable<Users> users,
            ILogger log)
        {

            log.LogInformation("Returning All Users...");
            return new OkObjectResult(users);
        }
    }
}
