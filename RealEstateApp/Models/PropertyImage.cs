using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealEstateApp.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? OriginalFileName { get; set; }

        [StringLength(100)]
        public string? ContentType { get; set; }

        public long FileSize { get; set; }

        [StringLength(50)]
        public string? FileExtension { get; set; }

        public bool IsWatermarkRemoved { get; set; }

        public bool IsPrimary { get; set; }

        [JsonIgnore]
        public virtual Property? Property { get; set; }

        [NotMapped]
        public string FilePath => $"/uploads/{FileName}";

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
