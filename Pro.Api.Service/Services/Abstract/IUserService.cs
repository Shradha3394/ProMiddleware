using Pro.Api.Model.Concrete;
namespace Pro.Api.Service.Services.Abstract;

public interface IUserService
{
    User? SignInByPassword(string logonName, string password, int partnerId);
}