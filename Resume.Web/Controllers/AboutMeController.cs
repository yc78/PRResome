using Microsoft.AspNetCore.Mvc;
using Resume.Bussines.Services.Interface;

namespace Resume.Web.Controllers
{
    public class AboutMeController : SiteBaseController
    {
        #region Fields

        private readonly IAboutMeService _aboutMeService;

        #endregion

        #region Constructor

        public AboutMeController(IAboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }

        #endregion

        #region Actions

        #region Index

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var model = await _aboutMeService.GetClientSideInfoAsync();
            return View(model);
        }

        #endregion

        #endregion
    }
}