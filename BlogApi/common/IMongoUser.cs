using BlogApi.Models;

namespace BlogApi.common
{
    public interface IMongoUser
    {
        public string ComputeHashedPassword(string password);
        public UserModel ValidateUser(string email, string password);
        public UserModel RegisterUser(UserModel user);
    }
}