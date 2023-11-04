using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.Models
{
    public class OrderItems
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NAME { get; set; }
        public int QUANTITY{ get; set; }
        public double PRICE{ get; set; }
        public double SUM { get { return QUANTITY * PRICE; } }
        
        //[ForeignKey("Orders")]
        //public int ORDER_ID { get; set; }
    }
}
