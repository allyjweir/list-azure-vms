using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAzureVMs
{
    public class ListAzureVMs
    {
        public static IEnumerable<IVirtualMachine> GetWorkers()
        {
            var azureHelper = new AzureHelper();
            var azure = azureHelper.GetAzureInstance();
            var virtualMachines = GetResourceGroupVirtualMachines(azure);
            return virtualMachines;
        }

        /// <summary>
        /// Returns list of all resources within a given Azure Resource Group.
        /// </summary>
        /// <param name="id">Id of the Azure Resource Group of interest. 
        /// Typically an ASCII, human defined and human readable value. Can
        /// retrieve from Azure Portal under 'Resource Group' blade.</param>
        /// <returns></returns>
        private static IEnumerable<IVirtualMachine> GetResourceGroupVirtualMachines(IAzure azure)
        {
            // NOTE: This is not normally how I manage secrect values. These are 
            // usually loaded from the environment. They have been placed here with
            // the aim of creating a minimum reproducable example.
            var resourceGroupId = "secretvaluehere";

            if (!azure.ResourceGroups
                    .CheckExistence(resourceGroupId))
            {
                throw new Exception($"Resource Group with id: " +
                    $"{resourceGroupId} does not exist");
            }

            var virtualMachines = azure
                .VirtualMachines
                .ListByResourceGroup(resourceGroupId);

            return virtualMachines;
        }

    }
}
