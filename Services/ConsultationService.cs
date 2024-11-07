using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly AppDBContext _dbContext;

        public ConsultationService(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<GetConcreteConsultationDTO> GetConcreteConsultation(Guid consultationId)
        {
            var consultation = await _dbContext.Consultations.Include(c => c.Speciality).FirstOrDefaultAsync(c => c.Id == consultationId);
            if (consultation == null)
            {
                throw new NotFoundException("Такой консультации не сущетсвует!");
            }
            var consultationComments = await _dbContext.InspectionComments
                .Where(c => c.ConsultationId == consultationId)
                .Select(c => new ConsultationCommentDTO
                {
                    Id = c.Id,
                    CreateTime = c.CreateTime,
                    ParentId = c.ParentId,
                    Content = c.Content,
                    AuthorId = c.Author.Id,
                    Author = c.Author.Name,
                    ModifiedDate = c.ModifyTime
                }).ToListAsync();
            foreach (var comment in consultationComments) { 

            }
            GetConcreteConsultationDTO concreteConsultation = new GetConcreteConsultationDTO()
            {
                Id = consultationId,
                CreateTime = consultation.CreateTime,
                InspectionId = consultation.InspectionId,
                Speciality = consultation.Speciality,
                Comments = consultationComments,

            };
            return concreteConsultation;
        }

        public async Task<Guid> CreateComment(Guid consultationId,Guid authorId, CreateConsultationCommentDTO commentDTO)
        {
            var consultation = await _dbContext.Consultations.Include(c => c.RootComment).FirstOrDefaultAsync(c => c.Id == consultationId);
            if (consultation == null)
            {
                throw new NotFoundException("Такой консультации не найдено!");
            }
            if (commentDTO.ParentId == null && consultation.RootCommentId != null)
            {
                throw new BadRequestException("Укажите корневой комментарий!");
            }
            if (commentDTO.Content.IsNullOrEmpty())
            {
                throw new BadRequestException("Поле content не заполнено!");
            }
            var author = await _dbContext.Doctors.FirstOrDefaultAsync(c => c.Id == authorId);
            if (author == null) {
                throw new BadRequestException("Вы не доктор!");
            }
            var comments = await _dbContext.InspectionComments.Where(i => i.ConsultationId == consultationId).ToListAsync();

            if (commentDTO.ParentId != null)
            {
                bool parentExists = comments.Any(c => c.Id == commentDTO.ParentId);
                if (!parentExists)
                {
                    throw new BadRequestException("Указанный родительский комментарий не существует!");
                }
            }

            InspectionComment comment = new InspectionComment()
            {
                Author = author,
                ConsultationId = consultationId,
                Content = commentDTO.Content,
                CreateTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ModifyTime = DateTime.UtcNow,
                ParentId = commentDTO.ParentId,
            };
            if (consultation.RootComment.Id == null)
            {
                consultation.RootComment = comment;
            }
            consultation.CommentsNumber++;
            await _dbContext.InspectionComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return comment.Id;
        }

        public async Task UpdateComment(Guid commentId, Guid doctorId, CommentDTO commentDTO)
        {
            var comment = await _dbContext.InspectionComments.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                throw new NotFoundException("Комментарий не найден!");
            }
            if (comment.Author.Id != doctorId)
            {
                throw new ForbiddenException("Вы не можете изменить не свой комментарий.");
            }

            comment.Content = commentDTO.Content;
            comment.ModifyTime = DateTime.UtcNow; 

            _dbContext.InspectionComments.Update(comment);
            await _dbContext.SaveChangesAsync();

        }
    }
}

