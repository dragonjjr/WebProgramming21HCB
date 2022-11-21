using Common;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAdministratorService
    {
        List<EmployeeInfoOutput> GetListEmployee(string searchName);
        EmployeeInfoOutput FindEmployeeById(int id);
    }
}
