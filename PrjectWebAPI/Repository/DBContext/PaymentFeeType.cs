using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class PaymentFeeType
    {
        public PaymentFeeType()
        {
            Recipients = new HashSet<Recipient>();
            TransactionBankings = new HashSet<TransactionBanking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual ICollection<TransactionBanking> TransactionBankings { get; set; }
    }
}
