﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        bool AddRecipient(RecipientInput recipientInput);

        bool DeleteRecipient(int id);
    }
}
