using Common;
using Org.BouncyCastle.Bcpg;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InternalTransferService
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

        public bool InternalTransfer(InternalTransfer model)
        {
            return _IinternalRepository.InternalTransfer(model);
        }

        public bool ExternalTransfer(ExternalTransfer model)
        {
            return _IinternalRepository.ExternalTransfer(model);
        }
    }
}
