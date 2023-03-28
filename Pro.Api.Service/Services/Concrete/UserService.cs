using Pro.Api.Model.Concrete;
using Pro.Api.Repository.Abstract;
using Pro.Api.Service.Services.Abstract;

namespace Pro.Api.Service.Services.Concrete;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? SignInByPassword(string logonName, string password, int partnerId)
    {
       return _userRepository.SignInByPassword(logonName, password, partnerId);
    }
}