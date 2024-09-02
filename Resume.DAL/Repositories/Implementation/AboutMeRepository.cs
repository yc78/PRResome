using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Resume.DAL.Context;
using Resume.DAL.Models.AboutMe;
using Resume.DAL.Repositories.Interface;
using Resume.DAL.ViewModels.AboutMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.DAL.Repositories.Implementation
{
    public class AboutMeRepository : IAboutMeRepository
    {
        #region Fields

        private readonly ResumeContext _context;

        #endregion

        #region Constructor

        public AboutMeRepository(ResumeContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task<AdminSideEditAboutMeViewModel?> GetInfoAsync()
        {
            var data = await _context.AboutMe
                .Select(aboutMe => new AdminSideEditAboutMeViewModel()
                {
                    BirthDate = aboutMe.BirthDate,
                    Email = aboutMe.Email,
                    FirstName = aboutMe.FirstName,
                    Id = aboutMe.Id,
                    LastName = aboutMe.LastName,
                    Location = aboutMe.Location,
                    Mobile = aboutMe.Mobile,
                    Position = aboutMe.Position,
                    Bio = aboutMe.Bio,
                    ImageName = aboutMe.ImageName
                })
                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<ClientSideEditAboutMeViewModel?> GetClientSideInfoAsync()
        {
            var data = await _context.AboutMe
                .Select(aboutMe => new ClientSideEditAboutMeViewModel()
                {
                    BirthDate = aboutMe.BirthDate,
                    Email = aboutMe.Email,
                    FirstName = aboutMe.FirstName,
                    Id = aboutMe.Id,
                    LastName = aboutMe.LastName,
                    Location = aboutMe.Location,
                    Mobile = aboutMe.Mobile,
                    Position = aboutMe.Position,
                    Bio = aboutMe.Bio,
                    ImageName = aboutMe.ImageName
                })
                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<AboutMe> GetAsync()
        {
            return await _context.AboutMe.FirstOrDefaultAsync();
        }

        public void Update(AboutMe aboutMe)
        {
            _context.AboutMe.Update(aboutMe);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
