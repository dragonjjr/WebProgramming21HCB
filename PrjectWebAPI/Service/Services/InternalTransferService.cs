using Common;
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
    }
}
