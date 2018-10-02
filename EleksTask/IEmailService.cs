using System.Threading.Tasks;

namespace EleksTask
{
    interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
