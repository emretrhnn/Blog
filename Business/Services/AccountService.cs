using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;

namespace Business.Services
{
    public interface IAccountService
    {
        Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel);
    }
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel)
        {
           var user = _userService.Query().SingleOrDefault(u => u.UserName == accountLoginModel.UserName &&
               u.Password == accountLoginModel.Password && u.IsActive);

            if(user is null)
            {
                return new ErrorResult("Invalid user name or password");
            }

            userResultModel.UserName = user.UserName;
            userResultModel.Role.Name = user.Role.Name;

            return new SuccessResult();
        }
    }
}
