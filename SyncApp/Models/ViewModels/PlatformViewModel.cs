using System;
using System.Collections.Generic;

namespace SyncApp.Models.ViewModels
{
    public class PlatformViewModel
    {
        public int Id { get; set; }
        public string UniqueName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public virtual ICollection<WellViewModel> Well { get; set; }
    }
}
