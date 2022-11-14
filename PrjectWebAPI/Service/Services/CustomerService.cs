using Common;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _IcustomerRepository;

        public CustomerService(ICustomerRepository IemployeeRepository)
        {
            _IcustomerRepository = IemployeeRepository;
        }

        public bool AddRecipient(RecipientInput recipientInput)
        {
            return _IcustomerRepository.AddRecipient(recipientInput);
        }
    }
}
