using Microsoft.AspNetCore.Mvc;
using Resume.Bussines.Extensions;
using Resume.Bussines.Services.Interface;

namespace Resume.Web.Areas.Admin.Components;

public class LeftSideBarViewComponent : ViewComponent
{
    #region Fields

    private readonly IUserService _userService;

    #endregion

    #region Constructor

    public LeftSideBarViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewData["User"] = await _userService.GetInformationAsync(User.GetUserId());
        return View("LeftSideBar");
    }

    #endregion
}