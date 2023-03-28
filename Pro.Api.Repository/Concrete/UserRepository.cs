using Pro.Api.Model.Concrete;
using Pro.Api.Repository.Abstract;

namespace Pro.Api.Repository.Concrete;
public class UserRepository : IUserRepository
{
    private List<User> users = new()
    {
        new User { UserId = 1234, FirstName = "Ram", LastName = "Singh", Email = "xyz1@abc.com", Password = "111111", PartnerId = 88},
        new User { UserId = 1111, FirstName = "Riya", LastName = "Singh", Email = "xyz2@abc.com", Password = "111111", PartnerId = 88 },
        new User { UserId = 4444, FirstName = "Rohan", LastName = "Singh", Email = "xyz3@abc.com", Password = "111111", PartnerId = 88 },
        new User { UserId = 3466, FirstName = "Rohit", LastName = "Singh", Email = "xyz4@abc.com", Password = "111111", PartnerId = 88 },
    };

    public User? SignInByPassword(string logonName, string password, int partnerId)
    {
        var user = users.FirstOrDefault(u =>
            u.Email.Equals(logonName) && u.Password.Equals(password) && u.PartnerId.Equals(partnerId));
        return user;
    }
}
