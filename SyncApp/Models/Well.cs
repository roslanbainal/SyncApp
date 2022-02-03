using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncApp.Models
{
    public class Well
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Platform")]
        public int PlatformId { get; set; }
        public string UniqueName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Platform Platform { get; set; }
    }
}