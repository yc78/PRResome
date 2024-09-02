using Microsoft.AspNetCore.Mvc;

namespace Resume.Web.Areas.Admin.Controllers;

public class HomeController:AdminBaseController
{
    #region Actions

    public IActionResult Index()
    {
        return View();
    }

    #endregion
}