using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public string UniqueName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<WellModel> Well { get; set; }
    }
}
