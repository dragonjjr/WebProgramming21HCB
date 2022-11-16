using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class OtpTable
    {
        public OtpTable()
        {
            Accounts = new HashSet<Account>();
            TransactionBankings = new HashSet<TransactionBanking>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TransactionId { get; set; }
        public string Otp { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<TransactionBanking> TransactionBankings { get; set; }
    }
}
