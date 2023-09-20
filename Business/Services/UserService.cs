using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IUserService : IService<UserModel>
    {
        List<UserModel> GetUsers();
    }

    public class UserService : IUserService
    {
        private readonly RepoBase<User> _userRepo;

        public UserService(RepoBase<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }

        public List<UserModel> GetUsers()
        {
            return Query().ToList();
        }

        public IQueryable<UserModel> Query()
        {
            return _userRepo.Query().OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId,
                    Role = new RoleModel()
                    {
                        Name = u.Role.Name,
                    }
                });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
