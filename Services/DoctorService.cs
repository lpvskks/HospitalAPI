using Microsoft.AspNetCore.Identity;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDBContext _dbContext;
        private readonly TokenInteraction _tokenHelper;
        public DoctorService(AppDBContext dbContext, TokenInteraction tokenHelper)
        {
            _dbContext = dbContext;
            _tokenHelper = tokenHelper;
        }
        public bool IsUniqueDoctor(string email)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(x => x.Email == email);
            if (doctor == null)
            {
                return true;
            }
            return false;
        }


        public async Task<TokenResponseDTO> Register(DoctorRegisterDTO doctorRegisterDTO)
        {
            Doctor doctor = new Doctor()
            {
                Id = Guid.NewGuid(),
                Name = doctorRegisterDTO.Name,
               // Password = doctorRegisterDTO.Password,
                Email = doctorRegisterDTO.Email,
                Birthday = doctorRegisterDTO.Birthday,
                Gender = doctorRegisterDTO.Gender,
                Phone = doctorRegisterDTO.Phone,
                CreateTime = DateTime.UtcNow
            };
            await _dbContext.Doctors.AddAsync(doctor); 
            await _dbContext.SaveChangesAsync();

            var token = _tokenHelper.GenerateToken(doctor);

            return new TokenResponseDTO
            {
                Token = token
            };

        }
    }
}
