using BlogApi.Models;
using System;

namespace BlogApi.common
{
    public class MongoUser : IMongoUser
    {
        public User AuthenticateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
