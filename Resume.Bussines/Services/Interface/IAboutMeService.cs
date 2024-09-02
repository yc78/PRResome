﻿using Resume.DAL.ViewModels.AboutMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Bussines.Services.Interface
{
    public interface IAboutMeService
    {
        #region Admin

        Task<AdminSideEditAboutMeViewModel?> GetInfoAsync();
       
        Task<ClientSideEditAboutMeViewModel?> GetClientSideInfoAsync();

        Task<AdminSideEditAboutMeResult> UpdateAsync(AdminSideEditAboutMeViewModel model);

        #endregion
    }
}
