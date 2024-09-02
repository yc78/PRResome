using Microsoft.EntityFrameworkCore;
using Resume.DAL.Context;
using Resume.DAL.Models.User;
using Resume.DAL.Repositories.Interface;
using Resume.DAL.ViewModels.User;
using System.Reflection;

namespace Resume.DAL.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly ResumeContext _contex;

    #endregion

    #region Constructor

    public UserRepository(ResumeContext contex)
    {
        _contex = contex;
    }

    #endregion

    #region Methods

    public async Task InsertAsync(User user)
    {
        await _contex.Users.AddAsync(user);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _contex.Users
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _contex.Users
            .FirstOrDefaultAsync(user => user.Email == email);
    }
    public async Task SaveAsync()
    {
        await _contex.SaveChangesAsync();
    }

    public async Task<bool> DuplicatedEmailAsync(int id, string email)
    {
        return await _contex.Users
            .AnyAsync(user => user.Email == email && user.Id != id);
    }

    public async Task<bool> DuplicatedMobileAsync(int id, string mobile)
    {
        return await _contex.Users
            .AnyAsync(user => user.Mobile == mobile && user.Id != id);
    }

    public void Update(User user)
    {
        _contex.Users.Update(user);
    }

    public async Task<FilterUserViewModel> FilterAsync(FilterUserViewModel model)
    {
        var query = _contex.Users.AsQueryable();

        #region Filter

        if (!string.IsNullOrEmpty(model.Email))
        {
            query = query.Where(user => EF.Functions.Like(user.Email,$"%{model.Email}%"));
        }

        if (!string.IsNullOrEmpty(model.Mobile))
        {
            query = query.Where(user => EF.Functions.Like(user.Mobile, $"%{model.Mobile}%"));
        }

        #endregion

        #region Paging

        await model.Paging(query.Select(user => new UserDetailsViewModel()
        {
            CreateDate = user.CreateDate,
            Email = user.Email,
            FirstName = user.FirstName,
            Id = user.Id,
            IsActive = user.IsActive,
            LastName = user.LastName,
            Mobile = user.Mobile
        }));

        #endregion

        return model;
    }

    #endregion

}