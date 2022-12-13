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
        bool CheckOTPTransaction(CheckOTPTransaction model);
        RecipientOutput ViewRecipientBySTK(string STK);

        UserViewModel GetListAccount(int UserID);
        bool InternalTransfer(InternalTransfer model);
        bool ExternalTransfer(ExternalTransfer model);
        bool ReceiveExternalTransfer(ExternalTransfer model);
        List<TransactionVM> GetListTransactionByAcount(string accountNumber, int typeTransaction);
    }
}
