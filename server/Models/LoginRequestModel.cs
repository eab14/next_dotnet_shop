using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class LoginRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
