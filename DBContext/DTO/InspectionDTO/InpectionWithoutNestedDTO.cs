namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class InpectionWithoutNestedDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime Date { get; set; }
        public DiagnosisDTO Diagnosis { get; set; }
    }
}
