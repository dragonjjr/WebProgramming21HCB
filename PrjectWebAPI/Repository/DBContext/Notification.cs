using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.DBContext
{
    public partial class Notification
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Content { get; set; }
        public string Stkreceive { get; set; }
        public string Stksend { get; set; }
    }
}
