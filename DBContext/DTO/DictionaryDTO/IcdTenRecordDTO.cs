using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO
{
    public class IcdTenRecordDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [JsonProperty("MKB_CODE")]
        public string? Code { get; set; }
        [JsonProperty("MKB_NAME")]
        public string Name { get; set; }

    }
}
