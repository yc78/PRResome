using Resume.DAL.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Bussines.Services.Interface
{
    public interface IContactUsService
    {
        Task<CreateContactUsResult> CreateAsync(CreateContactUsViewModel model);

        Task<FilterContactUsViewModel> FilterAsync(FilterContactUsViewModel model);

        Task<ContactUsDetailsViewModel> GetByIdAsync(int id);

        Task<AnswerResult> AnswerAsync(ContactUsDetailsViewModel model);
    }
}
