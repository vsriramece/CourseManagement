using System.Threading.Tasks;

namespace Chama.Common.Email
{
    public interface IEmailProvider
    {
        Task<bool> SendEmail(string recepients, string subject, string messageBody);
    }
}
