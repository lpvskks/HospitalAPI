﻿using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IInspectionService
    {
       public Task<Guid> CreateInspection(Guid patientId, Guid doctorId, InspectionCreateDTO inspectionCreateDTO);
    }
}
