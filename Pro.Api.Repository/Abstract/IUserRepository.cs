using Pro.Api.Model;
using Pro.Api.Model.Concrete;

namespace Pro.Api.Repository.Abstract;
public interface IUserRepository
{
    User? SignInByPassword(string logonName, string password, int partnerId);
}