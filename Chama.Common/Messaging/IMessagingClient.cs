using System.Threading.Tasks;

namespace Chama.Common.Messaging
{
    public interface IMessagingClient
    {
        Task<bool> PublishMessage<T>(T message);
    }

}
