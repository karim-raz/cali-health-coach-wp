using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
   // [Table("Details")]
   public class Details
    {



        //  [PrimaryKey]
        
        public string Id { get; set; }
       
        public string part1 { get; set; }
        public string part2 { get; set; }
        public string part3 { get; set; }
        public string part4 { get; set; }

        public string val1 { get; set; }
        public string val2 { get; set; }
        public string val3 { get; set; }
        public string val4 { get; set; }
    }
}
