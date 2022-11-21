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
    public class AdministratorService : IAdministratorService
    {
        private IAdministratorRepository _IAdministratorRepository;

        public AdministratorService(IAdministratorRepository _IAdministratorRepository)
        {
            this._IAdministratorRepository = _IAdministratorRepository;
        }

        public EmployeeInfoOutput FindEmployeeById(int id)
        {
            return this._IAdministratorRepository.FindEmployeeById(id);
        }

        public List<EmployeeInfoOutput> GetListEmployee(string searchName)
        {
            return this._IAdministratorRepository.GetListEmployee(searchName);
        }

    }
}