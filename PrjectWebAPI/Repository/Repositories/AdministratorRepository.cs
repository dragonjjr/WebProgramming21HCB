using Common;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly _6IVYVvfe0wContext dbConteemployeet;
        public AdministratorRepository(_6IVYVvfe0wContext _dbConteemployeet)
        {
            this.dbConteemployeet = _dbConteemployeet;
        }

        /// <summary>
        /// API Lấy danh sách nhân viên (Phân hệ quản trị viên). Nếu không tồn tại điều kiện tìm kiếm (tên) thì lấy tất cả
        /// </summary>
        /// <returns></returns>
        /// 
        public List<EmployeeInfoOutput> GetListEmployee(string searchName)
        {
            try
            {
                var records = dbConteemployeet.UserManages.Where(employee => employee.IsDeleted == false && employee.IsStaff == true && ((searchName != null && searchName != "") ? employee.Name.Contains(searchName) : true)).Select(employee => new EmployeeInfoOutput
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Cmnd = employee.Cmnd,
                    Address = employee.Address,
                    Stk = employee.Stk,
                    SoDu = employee.SoDu,
                    BankKind = employee.BankKind,
                    Email = employee.Email,
                    Phone = employee.Phone
                }).ToList();

                if (records != null)
                {
                    return records;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// API Tìm nhân viên theo id
        /// </summary>
        /// <returns></returns>
        public EmployeeInfoOutput FindEmployeeById(int id)
        {
            try
            {
                var record = dbConteemployeet.UserManages.Where(employee=>employee.Id==id && employee.IsStaff==true && employee.IsDeleted==false).Select(employee => new EmployeeInfoOutput
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Cmnd = employee.Cmnd,
                    Address = employee.Address,
                    Stk = employee.Stk,
                    SoDu = employee.SoDu,
                    BankKind = employee.BankKind,
                    Email = employee.Email,
                    Phone = employee.Phone
                }).Single();

                if (record != null)
                {
                    return record;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
