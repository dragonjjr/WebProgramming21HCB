using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class OtpTable
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TransactionId { get; set; }
        public string Otp { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool? Status { get; set; }
    }
}
