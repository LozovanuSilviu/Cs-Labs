using System.Text;
using Ciphers.Ciphers.Implementations.Asymmetric;
using Ciphers.Ciphers.Implementations.Classic;
using Ciphers.Ciphers.Implementations.Symmetric;
using CiphersServer.Entities;
using Google.Authenticator;

namespace CiphersServer;

public class CiphersService
{
    private List<User> Users { get; set; }
    private TwoFactorAuthenticator Authenticator { get; set; }
    private Affine affine { get; set; }
    private CaesarWithOneKey caesarOneKey { get; set; }
    
    private CaesarWithTwoKeys caesarTwoKeys { get; set; }
    private Vigenere vigenere { get; set; }
    private BlowFish blowFish { get; set; }
    private RsaKeyPair keyPair { get; set; }
    private  string keyPhrase ="Publicity";


    public CiphersService()
    {
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
        Authenticator = new TwoFactorAuthenticator();
        affine = new Affine();
        caesarOneKey = new CaesarWithOneKey();
        caesarTwoKeys = new CaesarWithTwoKeys();
        vigenere = new Vigenere();
        blowFish = new BlowFish(keyPhrase);
        keyPair = RSA.GenerateKeyPair(2048);
    }

    public string SetupAccount(string username, string password)
    {
        var currentUser = new User();
        var codeKey = "Invalid username or password";
        var valid = false;
        foreach (var user in Users)
        {
            if (user.email.Equals(username)& user.password.Equals(password))
            {
                currentUser = user;
                valid = true;
                break;
            }
        }

        if (valid)
        {
            var code =Authenticator.GenerateSetupCode("MyApp", currentUser.email, currentUser.secretKey, false);
             codeKey = code.ManualEntryKey; 
        }
        return codeKey;
    }

    public string Login(string username, string password)
    {
        var currentUser = new User();
        var codeKey = "Invalid username or password";
        var valid = false;
        foreach (var user in Users)
        {
            if (user.email.Equals(username)& user.password.Equals(password))
            {
                currentUser = user;
                valid = true;
                break;
            }
        }

        if (valid)
        {
            var rand = new Random();
            codeKey = rand.Next(1111, 9999).ToString();
            currentUser.LoginCode = codeKey;
        }

        return codeKey;
    }

    public string PerformAuthentication(string email, string loginCode, string authenticatorCode)
    {
        var message = "Incorrect authenticator code";
        var currentUser = new User();
        foreach (var user in Users)
        {
            if (user.email.Equals(email))
            {
                currentUser = user;
                break;
            }
        }

        if (!loginCode.Equals(currentUser.LoginCode))
        {
            return "Incorect login code";
        }

        var pin = Authenticator.GetCurrentPIN(currentUser.secretKey);
        if (pin.Equals(authenticatorCode))
        {
            message = "You successfully authenticate yourself";
            currentUser.Authenticated = true;
        }

        return message;
    }

    public string VerifyBasicAccount(string email)
    {
        var currentUser = new User();
        foreach (var user in Users)
        {
            if (user.email.Equals(email))
            {
                currentUser = user;
                break;
            }
        }

        if (currentUser.Authenticated.Equals(false))
        {
            return "You cant use this source, you are not authenticated";
        }

        if (!currentUser.role.Equals("basic"))
        {
            return "You dont have the permission to use this source";
        }

        return "success";
    }
    
    public string VerifyPremiumAccount(string email)
    {
        var currentUser = new User();
        foreach (var user in Users)
        {
            if (user.email.Equals(email))
            {
                currentUser = user;
                break;
            }
        }

        if (currentUser.Authenticated.Equals(false))
        {
            return "You cant use this source, you are not authenticated";
        }

        if (!currentUser.role.Equals("premium"))
        {
            return "You dont have the permission to use this source";
        }

        return "success";
    }

    public string AffineEncryption(string message)
    {
        var crypted = affine.Encrypt(message);
        return crypted;
    }

    public string AffineDecryption( string message)
    {
        var decrypted = affine.Decrypt(message);
        return decrypted;
    }

    public string CaesarOneKeyEncryption(string message)
    {
        var encrypted = caesarOneKey.Encrypt(message);
        return encrypted;
    }

    public string CaesarOneKeyDecryption(string message)
    {
        var decrypted = caesarOneKey.Decrypt(message);
        return decrypted;
    }

    public string CaesarWithTwoKeysEncryption(string message)
    {
        var encrypted = caesarTwoKeys.Encrypt(message);
        return encrypted;
    }

    public string CaesarWithTwoKeysDecryption(string message)
    {
        var decrypted = caesarTwoKeys.Decrypt(message);
        return decrypted;
    }

    public string VigenereEncryption(string message)
    {
        var encrypted = vigenere.Encrypt(message);
        return encrypted;
    }
    
    public string VigenereDecryption(string message)
    {
        var decrypt = vigenere.Decrypt(message);
        return decrypt;
    }

    public string BlowFishEncryption(string message)
    {
        var encypted = blowFish.Encrypt(message);
        return encypted;
    }

    public string BlowFishDecryption(string message)
    {
        var decrypted = blowFish.Decrypt(message);
        return decrypted;
    }

    public string Rc4Encryption(string message)
    {
        var rc = new Rc4(Encoding.ASCII.GetBytes(keyPhrase));
        var byteMessage = Encoding.ASCII.GetBytes(message);
        var length = byteMessage.Length;
        var encrypted = rc.Encrypt(byteMessage, length);
        var textRc4 = Convert.ToBase64String(encrypted);
        return textRc4;
    }

    public string Rc4Decryption(string message)
    {
        var rc = new Rc4(Encoding.ASCII.GetBytes(keyPhrase));
        var bytesMessage = Convert.FromBase64String(message);
        var length = bytesMessage.Length;
        var decrypted = rc.Decrypt(bytesMessage, length);
        var stringDecrypted = Encoding.ASCII.GetString(decrypted);
        return stringDecrypted;
    }

    public string RsaEncryption(string message)
    {
        var messageBytes = Encoding.Unicode.GetBytes(message);
        var encrypted = RSA.Encrypt(messageBytes, keyPair.Public);
        var response = Convert.ToBase64String(encrypted);
        return response;
    } 
    
    public string RsaDecryption(string message)
    {
        var messageBytes = Convert.FromBase64String(message);
        var encrypted = RSA.Decrypt(messageBytes, keyPair.Private);
        var response = Convert.ToBase64String(encrypted);
        return response;
    }
}