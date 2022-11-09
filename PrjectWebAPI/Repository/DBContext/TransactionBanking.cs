using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class TransactionBanking
    {
        public int Id { get; set; }
        public string Stksend { get; set; }
        public string Stkreceive { get; set; }
        public string Content { get; set; }
        public decimal Money { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentFeeTypeId { get; set; }
        public int? BankReferenceId { get; set; }
        public string Otp { get; set; }
        public DateTime? CeatedOtpdate { get; set; }
        public DateTime? ExpiredOtpdate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual BankReference BankReference { get; set; }
        public virtual PaymentFeeType PaymentFeeType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
