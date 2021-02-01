using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class UserRegisterationModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length 3")]
        public string UserName { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length 5")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum length 6")]
        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum length 6")]
        [Compare("Password", ErrorMessage = "confirm password should be same of password")]
        public string ConfirmPassword { get; set; }

        public string Roles { get; set; }
    }

    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

    }

    public class SignInModel
    {

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length 5")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Minimum length 6")]
        public string Password { get; set; }
    }
}
