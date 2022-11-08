using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Repository.DBContext
{
    [Table("User_manage")]
    public partial class UserManage
    {
        public UserManage()
        {
            DebtReminders = new HashSet<DebtReminder>();
            Recipients = new HashSet<Recipient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Cmnd { get; set; }
        public string Address { get; set; }
        public string Stk { get; set; }
        public decimal SoDu { get; set; }
        public string BankKind { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsStaff { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<DebtReminder> DebtReminders { get; set; }
        public virtual ICollection<Recipient> Recipients { get; set; }
    }
}
