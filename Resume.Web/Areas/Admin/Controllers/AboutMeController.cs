using Microsoft.AspNetCore.Mvc;
using Resume.Bussines.Services.Interface;
using Resume.DAL.ViewModels.AboutMe;

namespace Resume.Web.Areas.Admin.Controllers
{
    public class AboutMeController : AdminBaseController
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

        #region Edit

        public async Task<IActionResult> Edit()
        {
            var aboutMe = await _aboutMeService.GetInfoAsync();

            if (aboutMe == null)
                return NotFound();

            return View(aboutMe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminSideEditAboutMeViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result = await _aboutMeService.UpdateAsync(model);
            switch (result)
            {
                case AdminSideEditAboutMeResult.Success:
                    TempData[SuccessMessage] = "ویرایش درباره من با موفقیت انجام شد.";
                    return RedirectToAction(nameof(Edit));

                case AdminSideEditAboutMeResult.Error:
                    TempData[ErrorMessage] = "خطایی رخ داده است لطفا مجدد تلاش کنید.";
                    break;

                case AdminSideEditAboutMeResult.AboutMeNotFound:
                    TempData[ErrorMessage] = "درباه ی من پیدا نشد.";
                    break;
            }

            return View(model);
        }

        #endregion

        #endregion

    }
}
