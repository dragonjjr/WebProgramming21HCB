using Common;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAdministratorService
    {
        List<EmployeeInfoOutput> GetListEmployee(string searchName);
        EmployeeInfoOutput FindEmployeeById(int id);
        bool AddNewEmployee(EmployeeAccountInput employeeInfo);
        bool UpdateEmployeeInfo(int employeeId, EmployeeInfoInput employeeInfo);
        bool DeleteEmployee(int employeeId);
        List<TransactionVM> GetListTransaction();
    }
}
