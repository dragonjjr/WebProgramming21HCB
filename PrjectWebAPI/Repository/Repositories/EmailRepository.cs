using Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Repository.DBContext;
using Repository.Interfaces;
using System.Linq;
using static Common.Helpers;

namespace Repository.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration config;
        private readonly _6IVYVvfe0wContext dbContext;

        public EmailRepository(IConfiguration _config, _6IVYVvfe0wContext _dbContext)
        {
            this.config = _config;
            this.dbContext = _dbContext;
        }

        public int CheckEmailExits(string email)
        {
            return dbContext.UserManages.Where(x => x.Email == email).Select(x => x.Id).SingleOrDefault();
        }

        public string FindEmailById(int id)
        {
            return dbContext.UserManages.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
        }

        public bool SendMail(EmailDTO emailDTO)
        {
            try
            {
                //Send Mail
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config.GetSection("EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(emailDTO.To));
                email.Subject = emailDTO.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailDTO.Body };


                using var smtp = new SmtpClient();
                smtp.Connect(config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(config.GetSection("EmailUserName").Value, config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMailByEmail(string email)
        {
            int exitsEmail = CheckEmailExits(email);
            if (exitsEmail > 0)
            {
                OtpTable otp = new OtpTable();
                otp.UserId = exitsEmail;
                otp.Otp = GenerateOTP();
                otp.CreateDate = System.DateTime.UtcNow;
                otp.ExpiredDate = System.DateTime.UtcNow.AddHours(2);

                dbContext.OtpTables.Add(otp);
                dbContext.SaveChanges();

                EmailDTO emailDTO = new EmailDTO();
                emailDTO.To = email;
                emailDTO.Subject = "Reset your password";
                emailDTO.Body = "Hi,"
                    + "<p></p>"
                    + "Please use this below verification code to complete reset your password."
                    + "<p></p>"
                    + "<h2>"
                    + otp.Otp.ToString()
                    + "</h2>"
                    + "<p></p>"
                    + "Thank you.";

                SendMail(emailDTO);

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        ///  Gửi mail dựa vào id của user và id transaction_banking
        /// </summary>
        /// <returns></returns>
        public bool SendMailById(int id, int transactionId)
        {
            string email = FindEmailById(id);
            if (email != null)
            {
                OtpTable otp = new OtpTable();
                otp.TransactionId = transactionId;
                otp.Otp = GenerateOTP();
                otp.CreateDate = System.DateTime.UtcNow;
                otp.ExpiredDate = System.DateTime.UtcNow.AddMinutes(3);

                dbContext.OtpTables.Add(otp);
                dbContext.SaveChanges();

                EmailDTO emailDTO = new EmailDTO();
                emailDTO.To = email;
                emailDTO.Subject = "Confirm your transaction";
                emailDTO.Body = "Hi,"
                    + "<p></p>"
                    + "Please use this below verification code to complete your transation."
                    + "<p></p>"
                    + "<h2>"
                    + otp.Otp.ToString()
                    + "</h2>"
                    + "<p></p>"
                    + "Thank you.";

                SendMail(emailDTO);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
