using Microsoft.AspNetCore.Mvc;

namespace CiphersServer.Controllers;

[ApiController]
[Route("api")]
public class CiphersController : ControllerBase
{

    public CiphersService CiphersService { get; set; }

    public CiphersController(CiphersService ciphersService)
    {
        CiphersService = ciphersService;
    }
    
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

    [HttpPost("affineDecrypt/{email}")]
    public IActionResult DecryptAffine(string email, string message)
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
        var response = CiphersService.AffineDecryption(message);
        return Ok($"This is you decrypted message using affine cipher: {response}");
    }
    
    [HttpPost("caesarOneKeyEncrypt/{email}")]
    public IActionResult EncryptCaesarOneKey(string email, string message)
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
        var response = CiphersService.CaesarOneKeyEncryption(message);
        return Ok($"This is you encrypted message using caesar cipher with one key cipher: {response}");
    }
    
    [HttpPost("caesarOneKeyDecrypt/{email}")]
    public IActionResult DecryptCaesarOneKey(string email, string message)
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
        var response = CiphersService.CaesarOneKeyDecryption(message);
        return Ok($"This is you decrypted message using caesar with one key cipher: {response}");
    }
    
    [HttpPost("caesarTwoKeyEncrypt/{email}")]
    public IActionResult EncryptCaesarTwoKey(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.CaesarWithTwoKeysEncryption(message);
        return Ok($"This is you encrypted message using caesar cipher with two keys : {response}");
    }
    
    [HttpPost("caesarTwoKeyEncrypt/{email}")]
    public IActionResult DecryptCaesarTwoKey(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.CaesarWithTwoKeysDecryption(message);
        return Ok($"This is you decrypted message using caesar cipher with two keys : {response}");
    }
    
    [HttpPost("vigenereEncrypt/{email}")]
    public IActionResult EncryptVigenere(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.VigenereEncryption(message);
        return Ok($"This is you encrypted message using vigenere  cipher: {response}");
    }
    
    [HttpPost("vigenereDecrypt/{email}")]
    public IActionResult DecryptVigenere(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.VigenereDecryption(message);
        return Ok($"This is you decrypted message using vigenere  cipher: {response}");
    }
    
    [HttpPost("blowFishEncrypt/{email}")]
    public IActionResult EncryptBlowFish(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.BlowFishEncryption(message);
        return Ok($"This is you encrypted message using blowFish  cipher: {response}");
    }
    
    [HttpPost("blowFishDecrypt/{email}")]
    public IActionResult DecryptBlowFish(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.BlowFishDecryption(message);
        return Ok($"This is you decrypted message using blowFish  cipher: {response}");
    }
    
    [HttpPost("rc4Encrypt/{email}")]
    public IActionResult EncryptRc4(string email, string message)
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
        var response = CiphersService.Rc4Encryption(message);
        return Ok($"This is you encrypted message using rc4  cipher: {response}");
    }
    
    [HttpPost("rc4Decrypt/{email}")]
    public IActionResult DecryptRc4(string email, string message)
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
        var response = CiphersService.Rc4Decryption(message);
        return Ok($"This is you decrypted message using rc4  cipher: {response}");
    }
    
    [HttpPost("RsaEncrypt/{email}")]
    public IActionResult EncryptRsa(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.RsaEncryption(message);
        return Ok($"This is you encrypted message using rsa cipher: {response}");
    }
    
    [HttpPost("RsaDecrypt/{email}")]
    public IActionResult DecryptRsa(string email, string message)
    {
        var check = CiphersService.VerifyPremiumAccount(email);
        if (check.Equals("You cant use this source, you are not authenticated"))
        {
            return BadRequest(check);
        }
        else if (check.Equals("You dont have the permission to use this source"))
        {
            return BadRequest(StatusCode(StatusCodes.Status401Unauthorized));
        }
        var response = CiphersService.RsaDecryption(message);
        return Ok($"This is you decrypted message using rsa cipher: {response}");
    }
}