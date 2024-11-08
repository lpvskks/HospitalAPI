using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class NestedInspectionDTO
    {
        public Guid Id {  get; set; }
        public DateTime CreateTime { get; set; }
        public Guid PreviousId { get; set; }
        public DateTime Date { get; set; }
        public Conclusion Conclusion { get; set; }
        public Guid DoctorId { get; set; }
        public string Doctor { get; set; }
        public Guid PatientId { get; set; }
        public string Patient { get; set; }
        public DiagnosisDTO Diagnosis { get; set; }
        public Boolean HasNested { get; set; }
        public Boolean HasChain { get; set; }
    }
}
