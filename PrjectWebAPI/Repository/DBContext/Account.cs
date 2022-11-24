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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? OtpId { get; set; }

        public virtual UserManage IdNavigation { get; set; }
        public virtual OtpTable Otp { get; set; }
    }
}
