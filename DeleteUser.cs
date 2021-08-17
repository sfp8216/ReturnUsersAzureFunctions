using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace My.Functions
{
    public static class DeleteUser
    {
        [FunctionName("DeleteUser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb")] DocumentClient client,
            ILogger log)
        {
            string id = req.Query["id"];
            try
            {
                Document doc = await client.DeleteDocumentAsync(
                    UriFactory.CreateDocumentUri("UsersDB", "UsersContainer", id),
                    new RequestOptions { PartitionKey = new PartitionKey(id) });
                if (doc == null)
                {
                    return new OkObjectResult("{\"status\":\"deleted\"}");
                }
            }
            catch (Exception)
            {
                return new NotFoundObjectResult("{\"status\":\"not found\"}");
            }
            return new NotFoundObjectResult("{\"status\":\"not found\"}");
        }
    }
}
