namespace CiphersServer.Entities;

public class User
{
    public string email { get; set; }
    public string  password { get; set; }
    public string role { get; set; }
    public string secretKey { get; set; }
    public bool Authenticated { get; set; }
    public string LoginCode { get; set; }
}