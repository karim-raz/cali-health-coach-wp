using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
  //[Table("Nutrition")]
  public class Nutrition
    {

      //  [PrimaryKey]
        public string Id { get; set; }
       
        public string name  { get; set; }
        public string category { get; set; }
    }
}
