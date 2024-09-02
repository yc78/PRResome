using Resume.Bussines.Services.Interface;
using Resume.Bussines.Services.Interfaces;
using Resume.DAL.Models.ContactUs;
using Resume.DAL.Repositories.Interface;
using Resume.DAL.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Bussines.Services.Implementation
{
    public class ContactUsService : IContactUsService
    {
        #region Fields

        private readonly IContactUsRepository _contactUsRepository;
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;

        #endregion

        #region Constructor

        public ContactUsService(IContactUsRepository contactUsRepository, IViewRenderService viewRenderService, IEmailService emailService)
        {
            _contactUsRepository = contactUsRepository;
            _viewRenderService = viewRenderService;
            _emailService = emailService;
        }

        #endregion

        #region Methods

        public async Task<CreateContactUsResult> CreateAsync(CreateContactUsViewModel model)
        {
            ContactUs contactUs = new ContactUs()
            {
                Answer = null,
                CreateDate = DateTime.Now,
                Description = model.Description,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Title = model.Title,
            };

            await _contactUsRepository.InsertAsync(contactUs);
            await _contactUsRepository.SaveAsync();

            return CreateContactUsResult.Success;
        }

        public async Task<FilterContactUsViewModel> FilterAsync(FilterContactUsViewModel model)
        {
            return await _contactUsRepository.FilterAsync(model);
        }

        public async Task<ContactUsDetailsViewModel> GetByIdAsync(int id)
        {
            return await _contactUsRepository.GetInfoByIdAsync(id);
        }

        public async Task<AnswerResult> AnswerAsync(ContactUsDetailsViewModel model)
        {
            var contactUs = await _contactUsRepository.GetByIdAsync(model.ContactUsId);

            if (contactUs == null)
                return AnswerResult.ContactUsNotFound;

            if (string.IsNullOrEmpty(model.Answer))
                return AnswerResult.AnswerIsNull;

            contactUs.Answer = model.Answer;

            _contactUsRepository.Update(contactUs);
            await _contactUsRepository.SaveAsync();

            string body = await _viewRenderService.RenderToStringAsync("Emails/AnswerContactUs", model);
            await _emailService.SendEmail(contactUs.Email, "پاسخ به تماس با ما", body);

            return AnswerResult.Success;
        }

        #endregion
    }
}
