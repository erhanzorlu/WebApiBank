using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBank.Models.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Status = DataStatus.Inserted;
            CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }
    }
    
    public enum DataStatus
    {
        Inserted=1,Updated=2,Deleted=3
    }
}