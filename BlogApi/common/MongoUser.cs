using AutoMapper;
using BlogApi.Models;
using MongoDB.Driver;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BlogApi.common
{
    public class MongoUser : IMongoUser
    {
        private readonly IMongoDatabase db;
        private readonly string collectionName = "User";

        public MongoUser(string connectionString, string dbName)
        {
            MongoClient client = new MongoClient(connectionString);
            db = client.GetDatabase(dbName);
        }

        public UserModel RegisterUser(UserModel user)
        {
            IMongoCollection<UserModel> collection = db.GetCollection<UserModel>(this.collectionName);
            user.Password = ComputeHashedPassword(user.Password);
            collection.InsertOne(user);
            return user;
        }

        private string ComputeHashedPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return hashedPassword;
        }
    }
}