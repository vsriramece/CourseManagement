using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Common.Messaging
{
    public class AzureServiceBusQueueClient : IMessagingClient
    {
        private readonly IQueueClient ServiceBusQueueClient;
        public AzureServiceBusQueueClient(IQueueClient queueClient)
        {
            ServiceBusQueueClient = queueClient;
        }

        public async Task<bool> PublishMessage<T>(T message)
        {
            if (message == null)
            {
                // This can be modified to a custom exception
                throw new Exception("Message cannot be empty");
            }

            Message brokeredMsg = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            await ServiceBusQueueClient.SendAsync(brokeredMsg);
            return true;
        }
    }
}
