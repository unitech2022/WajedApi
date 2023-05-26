using System;
using WajedApi.Models;
using WajedApi.ViewModels;

namespace WajedApi.Serveries
{
	public interface IUserService
	{
		Task<object> Register(UserForRegister userForRegister);
		Task<object> IsUserRegistered(string UserName);
		Task<object> LoginAdmin(AdminForLoginRequest adminForLogin);
		Task<object> LoginUser(UserForLogin userForLogin);
		Task<object> RegisterAdmin(UserForRegister adminForRegister);
		Task<object> UpdateUser(UserForUpdate userForUpdate);

		Task<bool> UpdateDeviceToken(string Token,string UserId);
		Task<User> GetUser(string UserId);
	}
}

