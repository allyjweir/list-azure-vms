using System.Linq;
using Xunit;

namespace ListAzureVMs
{
    public class ListAzureVMsTests
    {
        [Fact]
        public void CanRetrieveWorkers()
        {
            var workers = ListAzureVMs.GetWorkers().ToList();
            Assert.Equal(0, workers.Count);
        }
    }
}
