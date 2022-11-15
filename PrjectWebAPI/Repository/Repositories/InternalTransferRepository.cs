using Common;
using Repository.DBContext;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class InternalTransferRepository:IInternalRepository
    {
        private readonly _6IVYVvfe0wContext dbContext;
        public InternalTransferRepository(_6IVYVvfe0wContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public List<PaymentFeeTypeVM> GetPaymentFeeType()
        {
            var rs = new List<PaymentFeeTypeVM>();
            var model = dbContext.PaymentFeeTypes.Where(x => x.IsDeleted == false).ToList();
            if(model.Count() > 0)
            {
                for(int i =0; i< model.Count(); i++)
                {
                    var row = new PaymentFeeTypeVM
                    {
                        ID = model[i].Id,
                        Name = model[i].Name
                    };
                    rs.Add(row);
                }    
            } 
            return rs;
        }

    }
}
