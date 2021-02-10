/* a small console application designed to show how the azure key vault and service principal works.
 * the function of this application is to render the value of a secret.
 Permissions for this application were set in the service principal*/
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyVaultUrl = "https://demovaultburnley.vault.azure.net/";
            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());
                        
            KeyVaultSecret secret = client.GetSecret("dbpaddpaw");
            Console.WriteLine(secret.Value);
            Console.ReadKey();
            
        }
    }
}
