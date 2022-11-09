using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Otp { get; set; }
        public DateTime? CreatedOtpdate { get; set; }
        public DateTime? ExpiredOtpdate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual UserManage IdNavigation { get; set; }
    }
}
