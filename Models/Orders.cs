using SQLite;

namespace ProjectNE.Models
{
    public class Orders
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string INVOICE_TYPE{ get; set; } 
        public DateTime DATE { get; set; }
        public string TITLE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public double SUM { get; set; }
        public string ITEMS_JSON{ get; set; }

    }
}
