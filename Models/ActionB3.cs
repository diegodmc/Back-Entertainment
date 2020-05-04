using System.Collections.Generic;

namespace Back_Entertainment.Models
{
  
  public class ActionB3
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int  Idt {get; set;}
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAbvName { get; set; }

    }

    public class MyClass
    {
        public List<ActionB3> data { get; set; }
    }
}