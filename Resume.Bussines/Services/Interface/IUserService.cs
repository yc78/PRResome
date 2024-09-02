using Resume.DAL.Models.User;
using Resume.DAL.ViewModels.Account;
using Resume.DAL.ViewModels.User;

namespace Resume.Bussines.Services.Interface;

public interface IUserService
{
    #region Methods

    Task<CreateUserResult> CreateAsync(CreateUserViewModel model);

    Task<EditUserViewModel> GetForEditByIdAsync(int id);

    Task<EditUserResult> UpdateAsync(EditUserViewModel model);

    Task<FilterUserViewModel> FilterAsync(FilterUserViewModel model);

    Task<LoginResult> LoginAsync(LoginViewModel model);

    Task<User> GetByEmailAsync(string email);

    Task<UserDetailsViewModel> GetInformationAsync(int id);

    #endregion
}