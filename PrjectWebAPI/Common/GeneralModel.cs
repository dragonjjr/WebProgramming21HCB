using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Helpers;

namespace Common
{
    public class GeneralModel
    {

    }

    public class ResponeseMessage{
        public bool IsSuccess { get; set; }
        public int Status { get; set; }

        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public UserViewModel Infor { get; set; }
    }

    public class UserViewModel
    {
        public string Name { get; set; }
        public string Cmnd { get; set; }
        public string Address { get; set; }
        public string Stk { get; set; }
        public decimal SoDu { get; set; }
        public string BankKind { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsStaff { get; set; }
    }

    public class RechargeInput
    {
        public int BankID { get; set; }
        public string STK_Send { get; set; }
        public string STK_Receive { get; set; }
        public decimal SoTien { get; set; }
        public string NoiDung { get; set; }
        public int PaymentTypeID { get; set; }
        public int TransactionTypeId { get; set; }
    }

    public class AccountInforInput
    {
        public int BankID { get; set; }
        public string STK { get; set; }
    }
    public class PaymentFeeTypeVM
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RecipientInput
    {
        public string STK { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
    }

    public class RecipientOutput
    {
        public int Id { get; set; }
        public string STK { get; set; }
        public string Name { get; set; }
    }

    public class RecipientEdit
    {
        public string STK { get; set; }
        public string Name { get; set; }
    }

    public class UserBalance
    {
        public string STK { get; set; }

        public decimal SoDu { get; set; }
    }


    public class EmailDTO
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class EmailInput
    {
        public string Email { get; set; }
    }
}
