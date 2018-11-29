using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ScopedDIProvider
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([QueueTrigger("myqueue-items", Connection = "")]string myQueueItem, ILogger log)
        {
            await FunctionScopedDIProvider.Using(async provider =>
            {
                // The provider here would work as expeceted including scoped lifetime.
                // Ex: var myService = provider.GetService<IMyService>();

                // TODO: You function logic goes here. 

                // A mock task completion as we don't have a real awaitable task here.
                await Task.CompletedTask;
            });

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
