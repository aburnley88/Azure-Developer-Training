﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace cosmos_sql
{
    class Program
    {
        static string database = "appdb";
        static string containername = "customer";
        static string endpoint = "https://appaccoutburnley.documents.azure.com:443/";
        static string accountkeys = "OH6QlsJ00O48MbTlhihGNirJ0nd5npcTOltV4q5prKOQCatOdrXtziJNVVt2YFoJei2WF9CRtMYr1eFYSyqmrA==";

        static async Task Main(string[] args)
        {
            //CreateNewItem().Wait();
            ReadItem().Wait();
            Console.ReadLine();
        }

        private static async Task CreateNewItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                customer obj = new customer(6, "Valerie", "Miami");
                obj.id = Guid.NewGuid().ToString();

                List<Orders> list_orders = new List<Orders>()
                {
                    new Orders(100,10), new Orders(101,20)
                };
                obj.order = list_orders;

                ItemResponse<customer> response = await container_conn.CreateItemAsync(obj);
                Console.WriteLine("Request charge is {0}", response.RequestCharge);
                Console.WriteLine("Customer added");
            }
        }

        private static async Task ReadItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                PartitionKey pk = new PartitionKey("Miami");
                string id = "bab5737e-8f16-4e8f-bfdd-bcb9ccf4f5ab";

                ItemResponse<customer> response = await container_conn.ReadItemAsync<customer>(id, pk);
                customer customer_obj = response.Resource;

                Console.WriteLine($"Customer Id is {customer_obj.customerid}");
                Console.WriteLine($"Customer name is {customer_obj.customername}");
                Console.WriteLine($"Customer city is {customer_obj.city}");
                foreach(Orders obj in customer_obj.order)
                {
                    Console.WriteLine($"Order Id is {obj.orderid}");
                    Console.WriteLine($"Order Quantity is {obj.quantity}");
                }

            }
        }


    }
}