using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerWay.Common.Entities
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? DuffelOfferId { get; set; }
        public Guid? SearchId { get; set; }
        public Search? Search { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
        public ICollection<Ancillary> Ancillaries { get; set; } = new List<Ancillary>();


    }
}
