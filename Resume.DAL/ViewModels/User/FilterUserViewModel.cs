using Resume.DAL.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Resume.DAL.ViewModels.User;

public class FilterUserViewModel : BasePaging<UserDetailsViewModel>
{
    [Display(Name = "موبایل")]
    public string? Mobile { get; set; }

    [Display(Name = "ایمیل")]
    public string? Email { get; set; }
}