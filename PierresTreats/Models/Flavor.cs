using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class Flavor
  {
    this.JoinEntities = new HashSet<FlavorTreat>();
  }
  
  public int FlavorId { get; set; }
  public string Type { get; set; }
  public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
  }
}