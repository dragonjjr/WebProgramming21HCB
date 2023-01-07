using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IInternalTransferService
    {
        List<PaymentFeeTypeVM> GetPaymentFeeType();
        Task<bool> CheckOTPTransaction(CheckOTPTransaction model, bool isInternalTranfer);
        RecipientOutput ViewRecipientBySTK(string STK);

        UserViewModel GetListAccount(int UserID);
        int InternalTransfer(InternalTransfer model);
        int ExternalTransfer(ExternalTransfer model);
        bool ReceiveExternalTransfer(ExternalTransfer model);
        List<TransactionVM> GetListTransactionByAcount(string accountNumber, int typeTransaction);
        TransactionVM GetInforTransaction(int transactionId);
    }
}

