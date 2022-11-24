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
        private readonly _6IVYVvfe0wContext dbContext;
        public AdministratorRepository(_6IVYVvfe0wContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// API Get list of employees by search condition (employee name). If search condition is empty, get all employees.
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        /// 
        public List<EmployeeInfoOutput> GetListEmployee(string searchName)
        {
            try
            {
                var records = dbContext.UserManages.Where(employee => employee.IsDeleted == false && employee.IsStaff == true && ((searchName != null && searchName != "") ? employee.Name.Contains(searchName) : true)).Select(employee => new EmployeeInfoOutput
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
        /// API Find employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeInfoOutput FindEmployeeById(int id)
        {
            try
            {
                var record = dbContext.UserManages.Where(employee => employee.Id == id && employee.IsStaff == true && employee.IsDeleted == false).Select(employee => new EmployeeInfoOutput
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
                }).SingleOrDefault();

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

        /// <summary>
        /// API Create a new employee information
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <returns></returns>
        public bool AddNewEmployee(EmployeeAccountInput employeeInfo)
        {
            try
            {

                // Add new employee info
                UserManage newEmployee = new UserManage();
                newEmployee.Name = employeeInfo.Infor.Name;
                newEmployee.Cmnd = employeeInfo.Infor.Cmnd;
                newEmployee.Address = employeeInfo.Infor.Address;
                newEmployee.Phone = employeeInfo.Infor.Phone;
                newEmployee.Email = employeeInfo.Infor.Email;
                newEmployee.CreatedDate = DateTime.Now;
                newEmployee.IsStaff = true;
                newEmployee.IsDeleted = false;

                dbContext.UserManages.Add(newEmployee);

                if (dbContext.SaveChanges() > 0)
                {
                    // Add new account info (role is 1)
                    Account newAccount = new Account();
                    newAccount.Id = newEmployee.Id;
                    newAccount.Username = employeeInfo.UserName;
                    newAccount.Role = "1";
                    newAccount.Password = BCrypt.Net.BCrypt.HashPassword(employeeInfo.Password);
                    newAccount.IsDeleted = false;

                    dbContext.Accounts.Add(newAccount);

                    if (dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// API Update a employee information
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <returns></returns>
        public bool UpdateEmployeeInfo(int employeeId, EmployeeInfoInput employeeInfo)
        {
            try
            {
                // Find employee info
                var employee = dbContext.UserManages.Where(employee => employee.Id == employeeId && employee.IsDeleted==false).SingleOrDefault();

                // Update employee info
                if (employee != null)
                {
                    employee.Name = employeeInfo.Name;
                    employee.Cmnd = employeeInfo.Cmnd;
                    employee.Address = employeeInfo.Address;
                    employee.Phone = employeeInfo.Phone;
                    employee.Email = employeeInfo.Email;
                    employee.UpdatedDate = DateTime.Now;

                    if (dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// API Delete a employee information
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                // Find account and employee info
                var account = dbContext.Accounts.Where(account => account.Id == employeeId && account.IsDeleted==false).SingleOrDefault();
                var employee = dbContext.UserManages.Where(employee => employee.Id == employeeId && employee.IsDeleted==false).SingleOrDefault();

                // Delete employee info
                if (account != null && employee != null)
                {
                    account.IsDeleted = true;
                    employee.IsDeleted = true;

                    if (dbContext.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// API Get List Transaction banking
        /// </summary>
        /// <returns></returns>
        public List<TransactionVM> GetListTransaction()
        {
            try
            {
                return dbContext.TransactionBankings
                            .Join(dbContext.TransactionTypes, d1 => d1.TransactionTypeId, d2 => d2.Id, (d1, d2) => new { d1.Id, d1.Stkreceive, d1.Stksend, d1.Content, d1.Money, d1.PaymentFeeTypeId, d1.BankReferenceId, TransactionTypeName = d2.Name })
                            .Join(dbContext.PaymentFeeTypes, d1 => d1.PaymentFeeTypeId, d2 => d2.Id, (d1, d2) => new { d1.Id, d1.Stkreceive, d1.Stksend, d1.Content, d1.Money, d1.TransactionTypeName, PaymentFeeTypeName = d2.Name, d1.BankReferenceId })
                            .Join(dbContext.BankReferences, d1 => d1.BankReferenceId, d2 => d2.Id,
                            (d1, d2) => new TransactionVM()
                            {
                                Id = d1.Id,
                                STKReceive = d1.Stkreceive,
                                STKSend = d1.Stksend,
                                Content = d1.Content,
                                Money = d1.Money,
                                TransactionType = d1.TransactionTypeName,
                                PaymentFeeType = d1.PaymentFeeTypeName,
                                BankReference = d2.Name
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }
    }
}
