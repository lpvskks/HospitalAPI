using System.Numerics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.AdditionalServices.HashPassword;
using webNET_2024_aspnet_1.AdditionalServices.Validators;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
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
            if (!IsUniqueDoctor(doctorRegisterDTO.Email))
            {
                throw new BadRequestException("Email уже используется");
            }

            if (!NameValidator.IsValidName(doctorRegisterDTO.Name))
            {
                throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
            }

            Doctor doctor = new Doctor()
            {
                Id = Guid.NewGuid(),
                Name = doctorRegisterDTO.Name,
                Password = HashPassword.HashingPassword(doctorRegisterDTO.Password),  
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
        public async Task<TokenResponseDTO> Login(LoginCredentialsDTO loginCredentialsDTO)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Email == loginCredentialsDTO.Email);
            if (doctor == null)
            {
                throw new BadRequestException("Неправильный Email или пароль");
            }
            else if (!HashPassword.VerifyPassword(loginCredentialsDTO.Password, doctor.Password))
            {
                throw new BadRequestException("Неправильный Email или пароль");
            }

            var token = _tokenHelper.GenerateToken(doctor);
            return new TokenResponseDTO
            {
                Token = token
            };

        }

        public async Task Logout(string token)
        {
            string id = _tokenHelper.GetIdFromToken(token);

            if (Guid.TryParse(id, out Guid doctorId) && doctorId != Guid.Empty)
            {
                await _dbContext.BlackTokens.AddAsync(new BlackToken { Blacktoken = token });
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new BadRequestException("Некорректный ID: не удалось извлечь или преобразовать id из токена.");
            }
        }

        public async Task<DoctorDTO> GetProfile(string token)
        {
            string doctorId = _tokenHelper.GetIdFromToken(token);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == Guid.Parse(doctorId));
            if (doctor != null)
            {
                return new DoctorDTO
                {
                    Id = doctor.Id,
                    CreateTime = doctor.CreateTime,
                    Name = doctor.Name,
                    Birthday = doctor.Birthday,
                    Gender = doctor.Gender,
                    Email = doctor.Email,
                    Phone = doctor.Phone
                };
            }
            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }

        }

        public async Task EditDoctorProfile(string token, DoctorEditDTO doctorEditDTO)
        {
            string doctorId = _tokenHelper.GetIdFromToken(token);

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == Guid.Parse(doctorId));
            if (doctor != null)
            {
                doctor.Email = doctorEditDTO.Email;
                doctor.Name = doctorEditDTO.Name;
                doctor.Birthday = doctorEditDTO.Birthday;
                doctor.Gender = doctorEditDTO.Gender;
                doctor.Phone = doctorEditDTO.Phone;

                await _dbContext.SaveChangesAsync();
            }

            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }
        }
    }
}
