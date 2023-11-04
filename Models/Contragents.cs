using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.Models
{
    public class Contragents
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DATE { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }

    }
}
