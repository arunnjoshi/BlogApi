using BlogApi.Models;

namespace BlogApi.common
{
    public interface IMongoUser
    {

        public User AuthenticateUser(User user);
    }

}
