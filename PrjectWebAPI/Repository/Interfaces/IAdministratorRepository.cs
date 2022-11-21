using Common;
using Repository.DBContext;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IAdministratorRepository
    {
        List<EmployeeInfoOutput> GetListEmployee(string searchName);
        EmployeeInfoOutput FindEmployeeById(int id);
    }
}
