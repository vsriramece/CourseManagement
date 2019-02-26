using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Common.Email
{
    public class EmailProvider : IEmailProvider
    {
        public Task<bool> SendEmail(string recepients, string subject, string messageBody)
        {
            // Mock implementation
            return Task.FromResult(true);
        }
    }
}
