using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
   // [Table("Tips")]
  public  class Tips
    {

       // [PrimaryKey]
     
        public string Id { get; set; }
 
        public string title1 { get; set; }
        public string title2 { get; set; }
        public string title3{ get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string desc3 { get; set; }

    }
}
