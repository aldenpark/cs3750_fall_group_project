using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project.Helpers
{
    //Security class used to encrypt/decrypt and hash strings
    //for more info on encryption watch https://www.youtube.com/watch?v=HlHDTQhVYoI
    //for more info on hashing watch https://www.youtube.com/watch?v=8eAD_UGLYaY&t=537s

    //In order for encryption to work you must add the following using statement: using Microsoft.AspNetCore.DataProtection;

    //In order for hashing to work, you must install the NuGet package: BCrypt.Net-Next

    public class Security
    {
        //Route value
        public readonly string UserPasswordRouteValue = "UserPasswordRouteValue";
        //Protector
        public readonly IDataProtector passwordProtector;
        //Protector provider, used to initialize the protector using a RouteValue
        public readonly IDataProtectionProvider dataProtectionProvider;
        
        //Constructor
        //Initializes the passwordProtector that is used to encrypt and decrypt a password
        //If in the future we need to encrypt other data fields, we will need to add another IDataProtector, and RouteValue
        //The RouteValue is used as an encryption key and should only map to one IDataProtector
        public Security()
        {
            dataProtectionProvider = DataProtectionProvider.Create("Security");

            passwordProtector = dataProtectionProvider.CreateProtector(UserPasswordRouteValue);

        }

        //Encrypts the password
        public string EncryptPassword(string password)
        {
            password = passwordProtector.Protect(password);

            return password;
        }

        //Hashes the password
        public string HashPassword(string password)
        {
            password = BCrypt.Net.BCrypt.HashPassword(password);

            return password;
        }

        //Decrypts the password
        public string DecryptPassword(string password)
        {
            password = passwordProtector.Unprotect(password);

            return password;
        }

        //Checks if unhashed password matches the hashed password
        //When the password is hashed, it can not be unhashed
        //If a user wants to log in, we must first encrypt the password that is sent in from the user and then use this method to check if the hashed password saved in the database matches the now encrypted password that was sent in by the user
        public bool IsMatch(string unhashedPassword, string hashedPassword)
        {
             return BCrypt.Net.BCrypt.Verify(unhashedPassword, hashedPassword);
        }

        public string EncryptString(string plainText)
        {
            string key = "db1535c08f173a43d82b9cf2349bd2c0";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptString(string cipherText)
        {
            string key = "db1535c08f173a43d82b9cf2349bd2c0";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }


    }
}
