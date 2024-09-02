using Microsoft.EntityFrameworkCore;
using Resume.DAL.Context;
using Resume.DAL.Models.ContactUs;
using Resume.DAL.Repositories.Interface;
using Resume.DAL.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.DAL.Repositories.Implementation
{
    public class ContactUsRepository : IContactUsRepository
    {
        #region Fields

        private readonly ResumeContext _context;

        #endregion

        #region Constructor

        public ContactUsRepository(ResumeContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task InsertAsync(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
        }

        public async Task<FilterContactUsViewModel> FilterAsync(FilterContactUsViewModel model)
        {
            var query = _context.ContactUs.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                query = query.Where(contactUs => EF.Functions.Like(contactUs.FirstName, $"%{model.FirstName}%"));
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                query = query.Where(contactUs => EF.Functions.Like(contactUs.LastName, $"%{model.LastName}%"));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(contactUs => EF.Functions.Like(contactUs.Email, $"%{model.Email}%"));
            }

            if (!string.IsNullOrEmpty(model.Mobile))
            {
                query = query.Where(contactUs => EF.Functions.Like(contactUs.Mobile, $"%{model.Mobile}%"));
            }

            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(contactUs => EF.Functions.Like(contactUs.Title, $"%{model.Title}%"));
            }

            switch (model.AnswerStatus)
            {
                case FilterContactUsAnswerStatus.All:
                    break;
                case FilterContactUsAnswerStatus.Answered:
                    query = query.Where(contactUs => contactUs.Answer != null);
                    break;

                case FilterContactUsAnswerStatus.NotAnswered:
                    query = query.Where(contactUs => contactUs.Answer == null);
                    break;
            }

            #endregion

            query = query.OrderByDescending(contactUs => contactUs.CreateDate);

            #region Pagination

            await model.Paging(query.Select(contactUs => new ContactUsDetailsViewModel()
            {
                Answer = contactUs.Answer,
                ContactUsId = contactUs.Id,
                Description = contactUs.Description,
                Email = contactUs.Email,
                FirstName = contactUs.FirstName,
                LastName = contactUs.LastName,
                Mobile = contactUs.Mobile,
                Title = contactUs.Title,
                CreateDate = contactUs.CreateDate
            }));

            #endregion

            return model;
        }

        public async Task<ContactUsDetailsViewModel> GetInfoByIdAsync(int id)
        {
            return await _context.ContactUs
                .Select(contactUs => new ContactUsDetailsViewModel()
                {
                    Answer = contactUs.Answer,
                    ContactUsId = contactUs.Id,
                    CreateDate = contactUs.CreateDate,
                    Description = contactUs.Description,
                    Email = contactUs.Email,
                    FirstName = contactUs.FirstName,
                    LastName = contactUs.LastName,
                    Mobile = contactUs.Mobile,
                    Title = contactUs.Title,
                })
                .FirstOrDefaultAsync(contactUs => contactUs.ContactUsId == id);
        }

        public async Task<ContactUs> GetByIdAsync(int id)
        {
            return await _context.ContactUs
                .FirstOrDefaultAsync(contactUs => contactUs.Id == id);
        }

        public async void Update(ContactUs contactUs)
        {
            _context.ContactUs.Update(contactUs);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
