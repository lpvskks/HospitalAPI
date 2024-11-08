namespace webNET_2024_aspnet_1.DBContext.Models.Enums
{
    using System.Text.Json.Serialization;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        CreateDesc,
        CreateAsc,
        NameAsc,
        NameDesc,
    }
}
