﻿using Common;
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

        public bool DeleteRecipient(int id)
        {
            return _IcustomerRepository.DeleteRecipient(id);
        }

        public List<RecipientOutput> GetListRecipientByUserId(int id)
        {
            return _IcustomerRepository.GetListRecipientByUserId(id);
        }

        public UserBalance GetUserBalance(int id)
        {
            return _IcustomerRepository.GetUserBalance(id);
        }
    }
}
