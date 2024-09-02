using Resume.Bussines.Security;
using Resume.Bussines.Services.Interface;
using Resume.DAL.Models.User;
using Resume.DAL.Repositories.Interface;
using Resume.DAL.ViewModels.Account;
using Resume.DAL.ViewModels.User;

namespace Resume.Bussines.Services.Implementation;

public class UserService : IUserService
{
    #region Fields

    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Methods

    public async Task<CreateUserResult> CreateAsync(CreateUserViewModel model)
    {
        User user = new User()
        {
            CreateDate = DateTime.Now,
            Email = model.Email.Trim().ToLower(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            IsActive = model.IsActive,
            Mobile = model.Mobile,
            Password = model.Password.Trim().EncodePasswordMd5(),
        };

        await _userRepository.InsertAsync(user);
        await _userRepository.SaveAsync();

        return CreateUserResult.Success;
    }

    public async Task<EditUserViewModel> GetForEditByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return null;


        return new EditUserViewModel()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            IsActive = user.IsActive,
            Mobile = user.Mobile
        };
    }

    public async Task<EditUserResult> UpdateAsync(EditUserViewModel model)
    {
        var user = await _userRepository.GetByIdAsync(model.Id);

        if (user == null)
            return EditUserResult.UserNotFound;

        if (await _userRepository.DuplicatedEmailAsync(user.Id, model.Email.ToLower().Trim()))
            return EditUserResult.EmailDuplicated;

        if (await _userRepository.DuplicatedMobileAsync(user.Id, model.Mobile))
            return EditUserResult.MobileDuplicated;

        user.Email = model.Email;
        user.Mobile = model.Mobile;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.IsActive = model.IsActive;

        _userRepository.Update(user);
        await _userRepository.SaveAsync();

        return EditUserResult.Success;
    }

    public async Task<FilterUserViewModel> FilterAsync(FilterUserViewModel model)
    {
        return await _userRepository.FilterAsync(model);
    }

    public async Task<LoginResult> LoginAsync(LoginViewModel model)
    {
        model.Email = model.Email.Trim().ToLower();

        var user = await _userRepository.GetByEmailAsync(model.Email);

        if (user == null)
        {
            return LoginResult.UserNotFound;
        }

        string hashPassword = model.Password.Trim().EncodePasswordMd5();

        if (user.Password != hashPassword)
        {
            return LoginResult.UserNotFound;
        }

        return LoginResult.Success;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        email = email.Trim().ToLower();

        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<UserDetailsViewModel> GetInformationAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return null;

        return new UserDetailsViewModel()
        {
            CreateDate = user.CreateDate,
            Email = user.Email,
            FirstName = user.FirstName,
            Id = user.Id,
            IsActive = user.IsActive,
            LastName = user.LastName,
            Mobile = user.Mobile
        };
    }

    #endregion

}