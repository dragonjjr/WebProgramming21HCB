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

    public class ResponeseMessagePartner
    {
        public DataPartnerAccount data { get; set; }
        public bool success { get; set; }
    }

    public class DataPartnerAccount
    {
        public string name { get; set; }
        public string paymentAccount { get; set; }
    }

    public class SendMoneyRequest
    {
        public string sendPayAccount { get; set; }
        public string sendAccountName { get; set; }
        public string receiverPayAccount { get; set; }
        public string payAccountFee { get; set; }
        public decimal transactionFee { get; set; }
        public decimal amountOwed { get; set; }
        public string bankReferenceId { get; set; }
        public string description { get; set; }
    }
}
