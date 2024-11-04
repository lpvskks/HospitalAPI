using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_2024_aspnet_1.DBContext.Models
{
    public class IcdTen
    {
        public Guid Id { get; set; }

        [Column("ID")]
        public int UnicalId { get; set; }

        [Column("REC_CODE")]
        public string? RecordCode { get; set; }

        [Column("MKB_CODE")]
        public string? Code { get; set; }

        [Column("MKB_NAME")]
        public string? Name { get; set; }

        [Column("ID_PARENT")]
        public int? ParentId { get; set; }

        [Column("ACTUAL")]
        public int? Actual { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}
