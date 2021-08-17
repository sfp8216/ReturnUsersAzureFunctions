using System.Linq;
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
using Microsoft.Azure.Documents.Linq;
using Poc1;
using Microsoft.Azure.Documents;
using System.Collections.Generic;

namespace My.Functions
{
    public static class SearchUsers
    {
        [FunctionName("SearchUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
               [CosmosDB(
                databaseName: "UsersDB",
                collectionName: "UsersContainer",
                ConnectionStringSetting = "myCosmosDb")
               ] DocumentClient client,
            ILogger log)
        {
            //Search by first name then last name then subscriber id then group id
            string searchTerm = req.Query["searchTerm"];
            Uri uri = UriFactory.CreateDocumentCollectionUri(databaseId: "UsersDB", collectionId: "UsersContainer");
            var options = new FeedOptions { EnableCrossPartitionQuery = true };

            var query = client.CreateDocumentQuery<Users>(uri, new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM UsersDB f WHERE f.FirstName LIKE @id OR f.LastName LIKE @id OR f.SubscriberID like @id OR f.GroupID LIKE @id",
                Parameters = new SqlParameterCollection(){
                    new SqlParameter("@id",searchTerm)
                }
            }, options).AsDocumentQuery();

            var results = new List<Users>();
            while (query.HasMoreResults)
            {
                foreach (Users user in await query.ExecuteNextAsync())
                {
                    results.Add(user);
                }
            }
            if (results is null || results.Count < 1)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(results);
        }
    }
}
