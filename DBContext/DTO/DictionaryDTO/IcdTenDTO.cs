using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO
{
    public class IcdTenDTO
    {
        [JsonProperty("ID")]
        public int UnicalId { get; set; }

        [JsonProperty("REC_CODE")]
        public string? RecordCode { get; set; }

        [JsonProperty("MKB_CODE")]
        public string? Code { get; set; }

        [JsonProperty("MKB_NAME")]
        public string? Name { get; set; }

        [JsonProperty("ID_PARENT")]
        public int? ParentId { get; set; }

        [JsonProperty("ACTUAL")]
        public int? Actual { get; set; }
    }
}
