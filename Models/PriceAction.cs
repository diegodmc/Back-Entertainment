using System.Collections.Generic;

namespace Back_Entertainment.Models
{
  public class PriceAction
    {
        public int Id { get; set; }
        public string Date {get; set;}
        public string Price {get; set;}
        public string Low {get; set;}
        public string High {get; set;}
        public string Var {get; set;}
        public string Varpct {get; set;}
        public string Vol {get; set;}
    }
    
    public class PriceActionData
    {
        public List<PriceAction> data { get; set; }
    }
}