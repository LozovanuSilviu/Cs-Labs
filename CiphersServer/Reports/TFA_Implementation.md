# 2 Factor Authentication and Role Based Access Control (RBAC)

## Theory
______________________
&ensp;&ensp;&ensp; Google authenticator - the easiest way to add another security layer and secure your online presence from hackers.

&ensp;&ensp;&ensp; Google authenticator allows you to quickly and conveniently protect your accounts by adding 2-factor authentication (2FA). The app brings together best in class security practices and seamless user experience together.


&ensp;&ensp;&ensp; It generates codes in a predefined time interval. This helps to protect your accounts from hackers, making your security bulletproof. Just enable the two-factor authentication in your account settings for your provider, just use the mobile app provides and you are good to go!

## Implementation
___________________
## Initial Steps
--------------------------------
&ensp;&ensp;&ensp; I simulated the database by using a in memory database (a simple list) which only the cipher service can access
```c#
private List<User> Users { get; set; }
```
and here is the User class with all it's properties
```c#
public class User
{
    public string email { get; set; }
    public string  password { get; set; }
    public string role { get; set; }
    public string secretKey { get; set; }
    public bool Authenticated { get; set; }
    public string LoginCode { get; set; }
}
```
&ensp;&ensp;&ensp;Users are created when the instance of Cipher Service is instantiated, here i give the property role which further is used for simulation of RBAC.
```c#
 var user1 = new User()
        {
            email = "basic@gmail.com",
            password = "lastlab",
            role = "basic",
            secretKey = "Pisica"
        };
        
        var user2 = new User()
        {
            email = "premium@gmail.com",
            password = "lastlab",
            role = "premium",
            secretKey = "Catel"
        };
        Users = new List<User>() {user1, user2};
```
## Create endpoints
--------------------------------
&ensp;&ensp;&ensp; __Endpoint for registering which is used when a new account should be linked to google authenticator app:__
```c#
 [HttpPost("setup/{username}/{password}")]
    public IActionResult Setup(string username, string password)
    {
        var response = CiphersService.SetupAccount(username, password);
        if (response.Equals("Invalid username or password"))
        {
            return BadRequest();
        }
        return Ok($"This is the key for your account {response}");
    }
```
&ensp;&ensp;&ensp; __Request and response:__
```text
  POST  https://localhost:7230/api/setup/basic@gmail.com/lastlab
```
```text
   This is the key for your account AHZ218ASD
```
&ensp;&ensp;&ensp; __Endpoint for login:__
```c#
[HttpPost("login/{username}/{password}")]
    public IActionResult Login(string username, string password)
    {
        var response = CiphersService.Login(username, password);
        if (response.Equals("Invalid username or password"))
        {
            return BadRequest("Invalid credentials");
        }

        return Ok($"This is your code for authentication:{response}");
    }
```
&ensp;&ensp;&ensp; __Request and response:__
```text
  POST  https://localhost:7230/api/login/basic@gmail.com/lastlab
```
```text
This is your code for authentication:4295
```

&ensp;&ensp;&ensp; __Endpoint for authentication which uses previously received code and the code which google authenticator is providing:__
```c#
[HttpPost("authentication/{email}/{loginCode}/{authenticatorCode}")]
    public IActionResult Authentication(string email,string loginCode, string authenticatorCode)
    {
        var response = CiphersService.PerformAuthentication(email, loginCode, authenticatorCode);
        if (response.Equals("Incorrect authenticator code"))
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
```
&ensp;&ensp;&ensp; __Request and response:__
```text
  POST   https://localhost:7230/api/authentication/basic@gmail.com/4295/649016
```

```text
This is your code for authentication:4295
```


&ensp;&ensp;&ensp; __Endpoints for ciphers(each work same way):__
```c#
 [HttpPost("affineEncrypt/{email}")]
    public IActionResult EncryptAffine(string email,string message)
    {
        var check = CiphersService.VerifyBasicAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.AffineEncryption( message);
        return Ok($"This is your encrypted message using affine cipher:{response}");
    }
```
&ensp;&ensp;&ensp; __Request and response:__
```text
  POST https://localhost:7230/api/affineEncrypt/basic@gmail.com?message=its not good to submit assignments after deadline
```
```text
 This is your encrypted message using affine cipher:rixufivffoifxtsjrihxxrvujzuixhkizmozhoyruz
```

## 2 Factor Authentication (2FA)
--------------------------
1. __Generate a Setup key if the account its not linked to the google authneticator app__

2. __Send login request, receive a code which will enable to use the authentication endpoint__
3. __Send authentication request with earlier received code and the code from google authenticator__

## Authorization (RBAC)
1. __It's given by default when we create users__
2. __When getting a request to a cipher endpoint, check the user role and process the result respectively__
```c#
 var currentUser = new User();
        foreach (var user in Users)
        {
            if (user.email.Equals(email))
            {
                currentUser = user;
                break;
            }
        }
        
        if (!currentUser.role.Equals("basic"))
        {
            return "You dont have the permission to use this source";
        }
```