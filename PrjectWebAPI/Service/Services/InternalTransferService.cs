using Common;
using Org.BouncyCastle.Bcpg;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InternalTransferService:IInternalTransferService
    {
        private IInternalRepository _IinternalRepository;
        public InternalTransferService(IInternalRepository IinternalRepository)
        {
            _IinternalRepository = IinternalRepository;
        }

        public List<PaymentFeeTypeVM> GetPaymentFeeType()
        {
            return _IinternalRepository.GetPaymentFeeType();
        }

        public bool CheckOTPTransaction(CheckOTPTransaction model)
        {
            return _IinternalRepository.CheckOTPTransaction(model);
        }

        public RecipientOutput ViewRecipientBySTK(string STK)
        {
            return _IinternalRepository.ViewRecipientBySTK( STK);
        }

        public UserViewModel GetListAccount(int UserID)
        {
            return _IinternalRepository.GetListAccount(UserID);
        }

        public int InternalTransfer(InternalTransfer model)
        {
            return _IinternalRepository.InternalTransfer(model);
        }

        public Task<bool> ExternalTransfer(ExternalTransfer model)
        {
            return _IinternalRepository.ExternalTransfer(model);
        }

        public bool ReceiveExternalTransfer(ExternalTransfer model)
        {
            return _IinternalRepository.ReceiveExternalTransfer(model);
        }

        public List<TransactionVM> GetListTransactionByAcount(string accountNumber, int typeTransaction)
        {
            return _IinternalRepository.GetListTransactionByAcount(accountNumber, typeTransaction);
        }

        public TransactionVM GetInforTransaction(int transactionId)
        {
            return _IinternalRepository.GetInforTransaction(transactionId);
        }
    }
}
