using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace cosmos_metrics
{
    class Program
    {
        static string database = "appdb";
        static string containername = "monitor";
        static string endpoint = "https://appaccount4000.documents.azure.com:443/";
        static string accountkeys = "zDVv6P9fb9LOEnmyvr756TFRMcpk2TB3kXAIgWnpIJui1ANnGFFq96QaGhmRKke81AbZqVklzx6jB2fEXIeSIA==";

        static void Main(string[] args)
        {
            ReadItem().Wait();
            Console.ReadLine();
        }

        static async Task ReadItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                
                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);
                                                
                string id = "71628e8d-1ee7-466b-bd78-b19660bdbacc";
                PartitionKey key=new PartitionKey("Administrative");
                ItemResponse<dynamic> p_response = await container_conn.ReadItemAsync<dynamic>(id,key);

                
                string output_string = JValue.Parse(p_response.Diagnostics.ToString()).ToString(Formatting.Indented);
                Console.WriteLine(output_string);
                Console.WriteLine("Operation complete");
                }
            }
    }
}
