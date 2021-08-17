using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Poc1;
using System.Linq;

namespace My.Functions
{
    public static class UpdateUser
    {
        [FunctionName("UpdateUser")]
        public static async Task<IActionResult> Run(
                 [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb")] DocumentClient client,
            ILogger log)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var userObject = JsonConvert.DeserializeObject<Users>(content);
            var id = userObject.id;

            Document selectDoc = client.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri("UsersDB", "UsersContainer"))
               .Where(user => user.Id == id)
               .AsEnumerable()
               .SingleOrDefault();

            Document doc = await client.ReplaceDocumentAsync(selectDoc.SelfLink, userObject);


            if (doc == null)
            {
                return new NotFoundObjectResult("{\"status\":\"not found\"}");
            }

            return new OkObjectResult(doc);
        }
    }
}
