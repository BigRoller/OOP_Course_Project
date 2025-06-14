using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class PickupPoint
  {
    public int PickupPointId { get; set; }

    public string City { get; set; }
    public string Street { get; set; }
    public string House { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool HasFittingRoom { get; set; }
    public DateTime NearestDeliveryDate { get; set; }

    public string FullAddress => $"г. {City}, ул. {Street}, д. {House}";
  }
}
