using Common;
using MimeKit;
using Service.Interfaces;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using static Common.Helpers;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public bool SendMailForAccount(string email)
        {
            return _emailRepository.SendMailByEmail(email);
        }

        public bool SendMailForTransaction(string stk, int transactionId)
        {
            return _emailRepository.SendMailBySTK(stk, transactionId);
        }
    }
}
