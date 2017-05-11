using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;

namespace ListAzureVMs
{
    public class AzureHelper
    {
        // NOTE: This is not how I normally store secret values. These are 
        // normally loaded from the environment but in the interests of 
        // creating a minimum reproducable, these have been placed here.
        public string SubscriptionId = "secretvaluehere";
        public string ClientId = "secretvaluehere";
        public string ClientSecret = "secretvaluehere";
        public string TenantId = "secretvaluehere";

        /// <summary>
        /// Returns an instance of <see cref="IAzure"/> based on given subscription and
        /// credentials.
        /// </summary>
        /// <param name="subscriptionId">Id of subscription that you want to 
        /// access/manipulate with the <see cref="IAzure"/> returned.</param>
        /// <returns></returns>
        public IAzure GetAzureInstance()
        {
            var credentials = GetAzureCredentials();
            return Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(SubscriptionId);
        }

        public string GetTestResourceGroupName()
            => $"zeigler-auto-testing-{DateTime.UtcNow.ToLongTimeString().Replace(":", "")}";

        /// <summary>
        /// Returns <see cref="AzureCredentials"/> based on values held in 
        /// application secrets .config file.
        /// <para>
        /// For more detail see docs/HowTo-GetAzureConfigurationValues.md which
        /// has a guide to retrieve the details and links to some websites that
        /// describe the process in further detail.
        /// </summary>
        /// <returns></returns>
        private AzureCredentials GetAzureCredentials()
        {
            return new AzureCredentials(new ServicePrincipalLoginInformation
            {
                ClientId=ClientId,
                ClientSecret=ClientSecret
            },
                TenantId,
                AzureEnvironment.AzureGlobalCloud);
        }

    }
}