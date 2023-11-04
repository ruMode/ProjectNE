using SQLite;

namespace ProjectNE.Models
{
    public class Warehouse
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set;}
        public DateTime DATE { get; set; }
        public string NAME { get; set; }
        public int QUANTITY { get; set; }
        //public double PRICE { get; set; }
        //public double SUM { get { return QUANTITY * PRICE; } }

        //[ForeignKey(typeof(Orders))]
        //public int ORDER_ID { get; set; }


    }
}
