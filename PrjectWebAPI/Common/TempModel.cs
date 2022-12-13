using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CheckOTPTransaction
    {
        public int TransactionID { get; set; }
        public string OTP { get; set; }
    }

    public class InternalTransfer
    {
        public int Send_UserID { get; set; }
        public string Send_STK { get; set; }
        public decimal Send_Money { get; set; }

        public string Receive_STK { get; set; }
        public string Content { get; set; }
        public int PaymentFeeTypeID { get; set; }
        public int TransactionTypeID { get; set; }
        public int? BankReferenceId { get; set; }
        public bool? isDebtRemind { get; set; }
    }

    public class ExternalTransfer
    {
        public int Send_UserID { get; set; }
        public string Send_STK { get; set; }
        public decimal Send_Money { get; set; }

        public int Receive_BankID { get; set; }
        public string Receive_STK { get; set; }
        public string Content { get; set; }
        public int PaymentFeeTypeID { get; set; }
        public int TransactionTypeID { get; set; }
        public int BankReferenceId { get; set; }
        public string RSA { get; set; }
    }
}
