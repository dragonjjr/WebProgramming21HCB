using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            TransactionBankings = new HashSet<TransactionBanking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TransactionBanking> TransactionBankings { get; set; }
    }
}
