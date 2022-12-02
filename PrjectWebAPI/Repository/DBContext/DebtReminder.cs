using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class DebtReminder
    {
        public int Id { get; set; }
        public string Stksend { get; set; }
        public decimal SoTien { get; set; }
        public string NoiDung { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Stkreceive { get; set; }
    }
}
