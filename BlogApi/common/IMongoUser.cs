using BlogApi.Models;

namespace BlogApi.common
{
    public interface IMongoUser
    {
        public UserModel RegisterUser(UserModel user);
    }
}