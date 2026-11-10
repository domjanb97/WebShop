using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComputerShopApi.Models
{
    public class Computer
    {
        [Key]
        [MaxLength(36)]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(37)")]
        public string? Brand { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? Type { get; set; }
        [Column(TypeName = "double")]
        public float Display { get; set; }
        public int Memory { get; set; }
        public DateTime CreatedTime { get; set; }

        [JsonIgnore]
        public Osystem Os { get; set; }
        public Guid OsId { get; set; }
    }
}
