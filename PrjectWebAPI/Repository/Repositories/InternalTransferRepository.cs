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

        /// <summary>
        /// Lấy danh sách hình thức thanh toán
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Kiểm tra OTP giao dịch chuyển tiền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckOTPTransaction(CheckOTPTransaction model)
        {
            try
            {
                if(model.TransactionID > 0 && model.OTP != string.Empty)
                {
                    var row = dbContext.TransactionBankings.Where(x => x.Id == model.TransactionID && x.Otp == model.OTP && x.CeatedOtpdate < DateTime.Now && x.ExpiredOtpdate <= DateTime.Now).FirstOrDefault();
                    if (row != null)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
